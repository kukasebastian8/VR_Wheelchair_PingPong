using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class _2v2Positiontargeter : MonoBehaviour
{
    public bool hitAIrecently =false;
    private float tempdistancez;
    private float tempdistancex;
    public float distancefromradius = 0.5f;
    private bool hittheballrecently;
    //ball to spawn for serving 
    public GameObject serveball;
    
    public NavMeshPath path;
    public bool playerteam =false;
    public bool isserving = false;
    public float Xlimit = 1.5f;
    // teannates targe and oponents 
    public GameObject target;
    public GameObject opponent1;
    public GameObject opponent2;
    public GameObject Teammate;
   
    private NavMeshAgent agent;
    private Transform temp;
    private Vector3 tempposition;
    public int randomvalue;
    public bool rolled = false;
    private Transform temptransform;
    public bool servinghit = false;
    // position values 
    private float x;
    private float y;
    private float z;
    public Vector3 center;
    public bool isxlimig1 = true;
    public float xmax;
    public bool oponent1player;
    public bool opponent2player;
    public int randomnum;
    public bool readytostart = false;
    public Vector3 j;
    private bool waittomove = true;
    public float zmin;
    public float zmax;
    private Vector3 ballsendpoint;
    private bool waittemp =true;
    public int scoreboardnum;
    public GameObject scoreboard;
    ScoreboardScript SBScript;
    private bool server;
    public bool innovolley = false;
    // Start is called before the first frame update

    private void Awake()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");
        SBScript = scoreboard.GetComponent<ScoreboardScript>();
        if (SBScript.getserve() == scoreboardnum)
        {
            destroynonserverinvert();
            isserving = true;
            server = true;

        }
        else
        {
            isserving = false;

        }
        
        if (SBScript.getserve() ==0)
        {
            destroynonserver();
            
        }
        else if (!isserving)
        {
            if (gameObject.name == "AI2v2 t2")
            {
                Destroy(gameObject);
            }
        }

        if (SBScript.getserve() == 2 || SBScript.getserve() == 3) {
            if ((SBScript.getaiScore() % 2) == 0)
            {
                if (scoreboardnum == 2)
                {
                    isserving = true;
                    server = true;
                }
                else if (scoreboardnum == 3)
                {
                    isserving = false;
                    server = false;
                }

            }
            else {
                if (scoreboardnum == 2)
                {
                    isserving = false;
                    server = false;
                    
                }
                else if (scoreboardnum == 3)
                {
                    isserving = true;
                    server = true;
                }
            }
        
        
        }
    }

    void Start()
    {
        if (gameObject.name == "AI2v2 t2")
        {
            opponent1.gameObject.GetComponent<_2v2Positiontargeter>().opponent1 = this.gameObject;
            opponent2.gameObject.GetComponent<_2v2Positiontargeter>().opponent1 = this.gameObject;
        }

            agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        if (Xlimit > 0) {
            isxlimig1 = true;
        }
        else {
            isxlimig1 = false;
        }

    }

    private void destroynonserver()
    {
        if (playerteam)
        {
            if (gameObject.name == "AI2v2 t2")
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    Destroy(gameObject);
                }
                else
                {


                }

            }
            else
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {

                }
                else
                {
                    Destroy(gameObject);

                }

            }
        }
        return;

    }
    private void destroynonserverinvert() {
        if (playerteam)
        {
            if (gameObject.name == "AI2v2 t2")
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    
                }
                else
                {
                    Destroy(gameObject);

                }

            }
            else
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    

                }

            }
        }
        return;

    }
    // Update is called once per frame
    void Update()
    {
        if (!readytostart)
        {
            StartCoroutine(waittostart());
        }
        else
        {

            if (isserving)
            {
                servetheball();
                
            }
            if (!waittomove) {
                targetball(); }
        }



    }
    private void servetheball()
    {

        j = transform.position;
        j.y = 3;
        if (playerteam)
        {
            j.x = 0.2f + j.x;

        }
        else {
            j.x = j.x - 0.2f;

        }
        target = Instantiate(serveball, j, Quaternion.identity);
        //target.GetComponent<ballcollision>().setservesequence();
        target.GetComponent<Positiondetection>().setplaying();
        isserving = false;
        servinghit = true;
        if (Teammate.tag == "AI")
        {
            Teammate.gameObject.GetComponent<_2v2Positiontargeter>().target = target;
        }
        if (opponent1.tag == "AI")
        {
            opponent1.gameObject.GetComponent<_2v2Positiontargeter>().target = target;
        }
        if (opponent2.tag == "AI")
        {
            opponent2.gameObject.GetComponent<_2v2Positiontargeter>().target = target;
        }
    }

    private void targetball() {
        if (!target)
            return;

        ballsendpoint = target.GetComponent<BallPredictor>().endPoint; // dont have to keep getting this 
        if (isxlimig1)// checks which side the AI is on 
        {
            if (!target.GetComponent<ballcollision>().bouncedopponent) {
                return;
                
            }
            else {
                servinghit = true;
                StartCoroutine(waittempstart());

            }
            if (waittemp) {
                return;
            }

            if (!target.GetComponent<ballcollision>().bouncedplayer)
            {
                servinghit = true;
            }
            else
            {
                servinghit = false;
                StartCoroutine(waittempstart());
            }
              if (waittemp) {
                return;
            }
            // on the positve x axis 
            if (ballsendpoint.x > Xlimit)
            {
                if (ballsendpoint.z > zmin && ballsendpoint.z < zmax) //if (Mathf.Abs(distance(ballsendpoint)) > distancefromradius) // makes sure that the endpoint is far enough from the other player
                {
                    agent.SetDestination(ballsendpoint);// sets destincation  to the balls endpoint
                    if (!(agent.CalculatePath(ballsendpoint, path)))// error prevention 
                    {
                        agent.SetDestination(target.gameObject.transform.position);

                    }
                  


                }
                else
                {
                    agent.SetDestination(center); // sets to center if this fails 


                }
            }
            else {


                agent.SetDestination(center);// sets to center if ball is on the other side of the field 

            }

        
          
        }

        else {
            if (!target.GetComponent<ballcollision>().bouncedplayer)
            {
                return;
            }
            else
            {
                servinghit = true;
                StartCoroutine(waittempstart());

            }
            if (waittemp)
            {
                return;
            }
            if (!target.GetComponent<ballcollision>().bouncedopponent)
            {
                servinghit = true;
            }
            else {
                servinghit = false;
            }
            // on the negative x axis 
            if (ballsendpoint.x < Xlimit)
            {
// Debug.Log("we Made it here");
                if (ballsendpoint.z > zmin && ballsendpoint.z < zmax)//if (Mathf.Abs(distance(ballsendpoint)) > distancefromradius  )
                {
                  

                    agent.SetDestination(ballsendpoint);// sets destincation  to the balls endpoint
                    if (!(agent.CalculatePath(ballsendpoint, path)))// error prevention 
                    {
                        agent.SetDestination(target.gameObject.transform.position );

                    }
                }
                else {
                    agent.SetDestination(center);// sets to center if this fails 


                }
            }
            else
            {


                agent.SetDestination(center);// sets to center if ball is on the other side of the field 

            }




        }


    }
    private float distance(Vector3 wayfrom) {
        tempdistancex = wayfrom.x - Teammate.transform.position.x;
        tempdistancez = wayfrom.z - Teammate.transform.position.z;
        tempdistancex *= tempdistancex;
        tempdistancez *= tempdistancez;
        return Mathf.Sqrt(tempdistancex + tempdistancez);
    }
    /*
    private void xyznottoobigorsmall()
    {
        if (x > xlimit)
            x = xlimit;
        if (x < -xlimit)
            x = -xlimit;
        if (z > zlimit)
            z = zlimit;
        if (z < -zlimit)
            z = -zlimit;
        if (y < -ylimit)
            y = -ylimit;
        if (y > ylimit)
            y = ylimit;

    }*/
    IEnumerator ballhitDelayRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        hitAIrecently = false;
    }
    IEnumerator waittostart()
    {
        yield return new WaitForSeconds(4f);
        readytostart = true;
        StartCoroutine(waittoxmove());
    }
    IEnumerator waittempstart()
    {
        yield return new WaitForSeconds(0.2f);
        waittemp = false;
        StartCoroutine(waittoxmove());
    }
    IEnumerator waittoxmove()
    {
        yield return new WaitForSeconds(0.5f);
        waittomove = false;
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ball" &&!hitAIrecently)
        {
            if (innovolley && coll.gameObject.GetComponent<ballcollision>().bounces == 0) {
                if (scoreboardnum < 2)
                {
                    if (!coll.gameObject.GetComponent<Positiondetection>().teamserve)
                    {
                        Debug.Log("Bounces == 0 & player serve"  );

                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();


                    }
                    else
                    {
                        Debug.Log("Bounces == 0 &  AI serve");
                        scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);
                    return;
                }
                else {
                    if (coll.gameObject.GetComponent<Positiondetection>().teamserve)
                    {
                        Debug.Log(" I dont know what this doese anymore ");
                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();


                    }
                    else
                    {
                        Debug.Log(" I dont know what this doese anymore but increase score ");
                        scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);
                    return;
                }


            }
            if (scoreboardnum < 2 && !server)
            {
                if (!coll.gameObject.GetComponent<ballcollision>().bouncedplayer)
                {
                    if (!coll.gameObject.GetComponent<Positiondetection>().teamserve)
                    {
                       
                        Debug.Log("Ai hit the ball and is not serving and served player served" );
                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();

                        
                    }
                    else {
                        Debug.Log("Ai hit the ball and is not serving and served AI served");
                        scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);
                }
            }
            else if(scoreboardnum > 1 && !server)
            {
                if (!coll.gameObject.GetComponent<ballcollision>().bouncedopponent)
                {
                    if (coll.gameObject.GetComponent<Positiondetection>().teamserve)
                    {
                        Debug.Log("Ai hit the ball and is not serving and served AI served  xxxx");
                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();


                    }
                    else
                    {
                        Debug.Log("Ai hit the ball and is not serving and served player served xxxxx");
                        scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);

                }
            }
 
            
                
            if (scoreboardnum > 1)
            {
                coll.gameObject.GetComponent<Positiondetection>().AIhistlast();
            }
            else {

                coll.gameObject.GetComponent<Positiondetection>().playerhitlast();
            }
            
            randomnum = Random.Range(0, 2);
            if (server) {
                if (scoreboardnum == 1) { // player AI 
                    if (gameObject.name == "AI2v2 t2")
                    {
                        randomnum = 0;
                    }
                    else {
                        randomnum = 1;
                    }
                }
                else if (scoreboardnum == 2) // opponet AI 1 these dont change because they dont have to move
                    randomnum = 1;
                else if (scoreboardnum == 3) // oppoent AI 2 
                {
                    randomnum = 0;
                }
               
            }
            if (randomnum != 1)
            {
                x = opponent1.transform.position.x - this.transform.position.x;
                z = opponent1.transform.position.z - this.transform.position.z;
                x = x / 2;
                z = z / 2;
                y = 2.5f;
            }
            else {
                x = opponent2.transform.position.x - this.transform.position.x;
                z = opponent2.transform.position.z - this.transform.position.z;
                x = x / 2;
                z = z / 2;
                y = 2.5f;
            }

            if (servinghit)
            {
                x = x / 1.2f;
                z = z / 1.2f;
                y = 1.9f;
                servinghit = false;

            }
                coll.rigidbody.velocity = new Vector3(0, 0, 0);
                coll.rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
                server = false;
                isserving = false;
                hitAIrecently = true;
                coll.gameObject.GetComponent<ballcollision>().bounces = 0;
                coll.gameObject.GetComponent<Positiondetection>().bounced = false;
                StartCoroutine(ballhitDelayRoutine());
            
        }
    }
}
