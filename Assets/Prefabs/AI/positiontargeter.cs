using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class positiontargeter : MonoBehaviour
{
    public GameObject scoreboard;
    public bool hitAIrecently = false;
    private bool hittheballrecently;
    public GameObject serveball;
    public bool isserving = false;
    public bool servinghit = false;
    public float Xlimit = 1.5f;
    public GameObject target;
    public GameObject playerframe;
    public GameObject player;
    private NavMeshAgent agent;
    private Transform temp;
    private Vector3 tempposition;
    public int randomvalue;
    public bool rolled = false;
    private Transform temptransform;
    public float x;
    public float y;
    public float z;
    public Vector3 addforce;
    public Vector3 j;
    public bool readytostart = false;
    public bool shorthit;
    public bool rightsidestart;
    private Vector3 altpotiion;
    public bool innovolley = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");
        

        // DETERMINE SERVE
        agent =  GetComponent<NavMeshAgent>();
        if (scoreboard.GetComponent<ScoreboardScript>().getserve() == 2)
        {
            isserving = true;

        }
        else
        {
            isserving = false;
            shorthit = true;
        }
        findstartposition();
        setstartposition();

        // END DETERMINE SERVE
    }
    private void findstartposition() {
        if (isserving)
        {
            if ((scoreboard.GetComponent<ScoreboardScript>().getaiScore() % 2) == 0)
            {
                rightsidestart = true;
            }
            else
            {
                rightsidestart = false;
            }
        }
        else {
            if ((scoreboard.GetComponent<ScoreboardScript>().getplayerScore() % 2) == 0)
            {
                rightsidestart = true;
            }
            else
            {
                rightsidestart = false;
            }

        }

    }
    private void setstartposition() {

        if(rightsidestart)
        {
            altpotiion = new Vector3(3.5f, 0.157f, 0f);
            transform.position = altpotiion;

        }
        else
        {
            altpotiion = new Vector3(3.7f, 0.157f, 0f);
            transform.position = altpotiion;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // gives the player time to get ready before the serve
        if ( !readytostart)
        {
            StartCoroutine(waittostart());
        }

        // for serving 
        if (isserving && readytostart) {
            servetheball();
        }
        move();
       
    }
    IEnumerator waittostart() {
        yield return new WaitForSeconds(3);
        readytostart = true;
    }
    IEnumerator ballhitDelayRoutine()
    {
        yield return new WaitForSeconds(1);
        hitAIrecently = false;
    }
    private void servetheball()
    {
      j = transform.position;
        j.y = 5;
        target = Instantiate(serveball,j, Quaternion.identity);
        // target.GetComponent<ballcollision>().setservesequence();
        target.GetComponent<Positiondetection>().setplaying();
        isserving =false;
        servinghit = true;
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ball") {
            if (innovolley && coll.gameObject.GetComponent<ballcollision>().bounces == 0)
            {
                if (coll.gameObject.GetComponent<Positiondetection>().teamserve)
                {
                    coll.gameObject.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                }
                else {
                    coll.gameObject.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                }
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }

            if ( !hitAIrecently)
            {
                // clears the ball state and sets it correctly 
                coll.gameObject.GetComponent<Positiondetection>().setbouncedfalse();
                coll.gameObject.GetComponent<Positiondetection>().AIhistlast();
                if (!coll.gameObject.GetComponent<ballcollision>().bouncedopponent && !servinghit && !hitAIrecently)
                {
                    if (coll.gameObject.GetComponent<Positiondetection>().teamserve)
                    {
                        coll.gameObject.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                    }
                    else {
                        coll.gameObject.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);

                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);

                }
                x = player.transform.position.x - this.transform.position.x;
                z = player.transform.position.z - this.transform.position.z;
                if (servinghit || shorthit)
                {
                    x = x / 2;
                    z = z / 2;
                    y = 1.9f;
                  
                    coll.rigidbody.velocity = new Vector3(0, 0, 0);
                    coll.rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
                    //coll.rigidbody.AddForce(new Vector3(0, y, 0), ForceMode.Impulse);
                    coll.gameObject.GetComponent<ballcollision>().bounces = 0;
                    coll.gameObject.GetComponent<Positiondetection>().bounced = false;
                    servinghit =false;
                    shorthit = false;
                }
                else
                {
                    x = x / 2;
                    z = z / 2;
                    y = 2.5f;
                    coll.rigidbody.velocity = new Vector3(0, 0, 0);
                    coll.rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
                    coll.gameObject.GetComponent<ballcollision>().bounces = 0;
                    coll.gameObject.GetComponent<Positiondetection>().bounced = false;
                }
                //xyznottoobigorsmall();
                //coll.gameObject.  ballrigid.AddForce((new Vector3(x, y, z), ForceMode.Impulse);

   
                hitAIrecently = true;

                StartCoroutine(ballhitDelayRoutine());
            }
             }

    }

    void move() {
        if (!target)
            return;
            if (!target.GetComponent<ballcollision>().bouncedopponent)
            {
                return;
            }

    
                if (!rolled)
                {
                    randomvalue = Random.Range(1, 3);
                    rolled = true;
                }

                if (randomvalue == 2)
                {
                    tempposition = playerframe.transform.position;
                    tempposition.x = tempposition.x * -1;

                    agent.SetDestination(tempposition);
                }
                else
                {
                    tempposition = playerframe.transform.position;
                    tempposition.x = tempposition.x * -1;
                    tempposition.z = (tempposition.z * -1) + 0.1f;
                    agent.SetDestination(tempposition);
                }

            
            if (target.GetComponent<BallPredictor>().endPoint.x > Xlimit)
            {
                agent.SetDestination(target.GetComponent<BallPredictor>().endPoint);
            }
            // mirror player we probably dont want to do this all of the time
            if (target.GetComponent<BallPredictor>().endPoint.x > 0)
            {
                rolled = false;

            }

            else if (target.GetComponent<BallPredictor>().endPoint.x < 0)
            {
                if (!rolled)
                {
                    randomvalue = Random.Range(1, 3);
                    rolled = true;
                }

                if (randomvalue == 2)
                {
                    tempposition = playerframe.transform.position;
                    tempposition.x = tempposition.x * -1;

                    agent.SetDestination(tempposition);
                }
                else
                {
                    tempposition = playerframe.transform.position;
                    tempposition.x = tempposition.x * -1;
                    tempposition.z = (tempposition.z * -1) + 0.1f;
                    agent.SetDestination(tempposition);
                }
            }
        

    }
}
