using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.OpenXR.Input;

public class ballcollision : MonoBehaviour
{
    public bool hitAIrecently = false;
    public GameObject player;
    bool isservesequence = false;
    public Rigidbody ballrigid;
    public Vector3 direction;
    public float x;
    public float y;
    public float z;
    public float xlimit = 20f;
    public float ylimit = 20f;
    public float zlimit = 20f;
    public bool bouncedplayer = false;
    public bool bouncedopponent = false;
    public int bounces = 0;
    private bool hitgroundtoorecently = false;
    public bool turretball = false;
    private GameObject novollyzone;
    // Start is called before the first frame update
    void Start()
    {
        novollyzone = GameObject.FindWithTag("NoVolleyPlayer");

        player = GameObject.FindWithTag("playertag");
        ballrigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindWithTag("Playertag");
    }
    void OnCollisionEnter(Collision coll)
    {

        Debug.Log(coll.gameObject.name);
        // If ball has hit ball recently and ball collides with court
        if (!hitgroundtoorecently && coll.gameObject.name == "Court")
        {




            bool ifAIServe = this.GetComponent<Positiondetection>().teamserve; // player = false AI = true  
            bool AIhitlast = this.GetComponent<Positiondetection>().whohitlast; // player = false AI = true 

            if (transform.position.x < -0.5 && !turretball && this.GetComponent<Positiondetection>().isinboundszaxis() && (this.GetComponent<Positiondetection>().checkboundsx() == 0))
            {
                bouncedplayer = true;

                if (bounces == 0 && !AIhitlast)
                {


                    if (ifAIServe)
                    {
                        Debug.Log("bounces = " + bounces + " AI served " + transform.position.x + "Player hit last");
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                    }
                    else
                    {
                        Debug.Log("bounces = " + bounces + " player served " + transform.position.x + "Player hit last");
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                    }

                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);


                    /* if (ifAIServe)
                     {

                     //    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                    //     string currentSceneName = SceneManager.GetActiveScene().name;
                      //   SceneManager.LoadScene(currentSceneName);
                     }
                     else {
                         this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                         string currentSceneName = SceneManager.GetActiveScene().name;
                         SceneManager.LoadScene(currentSceneName);
                     }*/
                }

            }
            else if (transform.position.x > -0.5 && !turretball && this.GetComponent<Positiondetection>().isinboundszaxis() && (this.GetComponent<Positiondetection>().checkboundsx() == 0))
            {
                bouncedopponent = true;
                if (bounces == 0 && AIhitlast)
                {



                    if (ifAIServe)
                    {
                        Debug.Log("bounces = " + bounces + " AI Served " + transform.position.x + "AIhitlast");
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                    }
                    else
                    {
                        Debug.Log("bounces = " + bounces + " player served " + transform.position.x + "AIhitlast");
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                    }
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);



                    /*
                    if (!ifAIServe)
                    {

                      //  this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                     //   string currentSceneName = SceneManager.GetActiveScene().name;
                   //     SceneManager.LoadScene(currentSceneName);
                    }
                    else
                    {
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                        string currentSceneName = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(currentSceneName);
                    }*/
                }

            }

            bounces += 1;

            //Debug.Log("ffffffxxxxxxxxxxfffffffffff");
            /*
            if (transform.position.x > -0.5 && AIhitlast && !turretball && this.GetComponent<Positiondetection>().isinboundszaxis() && (this.GetComponent<Positiondetection>().checkboundsx() == 0))
            {

               
                if (ifAIServe)
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                }
                else {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                }
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            else if (transform.position.x < -0.5 && !AIhitlast && !turretball && this.GetComponent<Positiondetection>().isinboundszaxis() &&(this.GetComponent<Positiondetection>().checkboundsx() ==0) ) { 
                 ifAIServe = this.GetComponent<Positiondetection>().teamserve; // player = false AI = true 
                if (ifAIServe)
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                }
                else
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                }

                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);

            }
            */
            if (bounces > 2)
            {
                if (turretball)
                {
                    Destroy(gameObject);
                    return;
                }
                ifAIServe = this.GetComponent<Positiondetection>().teamserve; // player = false AI = true 
                if (this.transform.position.x > -.5 && !turretball)
                {

                    if ((!AIhitlast && !ifAIServe) || (AIhitlast && !ifAIServe))
                    {
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                    }
                    else if (!AIhitlast && ifAIServe || AIhitlast && ifAIServe)
                    {
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();

                    }
                }
                else
                {
                    if ((!AIhitlast && !ifAIServe) || (AIhitlast && !ifAIServe))
                    {
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                    }
                    else if (!AIhitlast && ifAIServe || AIhitlast && ifAIServe)
                    {
                        this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);


                    }

                }
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);




            }
            hitgroundtoorecently = true;
            StartCoroutine(ballhitgroundDelayRoutine());
        }

        if (coll.gameObject.name == "PicklePaddle_Final")
        {

            bool ifAIServe = this.GetComponent<Positiondetection>().teamserve;

            if (novollyzone.GetComponent<NoVolleyZoneTrigger>().GetVollyFlag() && bounces == 0)
            {
                Debug.Log("Novolley Player");
                if (!ifAIServe)
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                }
                else
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                }
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);

            }
            if (!bouncedplayer && !isservesequence && !hitAIrecently)
            {
                // Debug.Log("how did we even get here at the moment");
                Debug.Log("player hit the ball before it bounced ");
                if (!ifAIServe)
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().ChangeServing();

                }
                else
                {
                    this.GetComponent<Positiondetection>().scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                }
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            if (!hitAIrecently)
            {
                if (isservesequence)
                {
                    Debug.Log("Hit the ball when the player is serving ");
                    this.gameObject.GetComponent<Positiondetection>().playerhitlast();
                    this.gameObject.GetComponent<Positiondetection>().setbouncedfalse();
                    // solution 1
                    // direction = (transform.position - coll.transform.position).normalized;

                    direction = coll.gameObject.GetComponent<controllervelocity>().getvelocity();
                    //ballrigid.velocity = new Vector3(0, 0, 0);
                    //ballrigid.AddForce(direction * 4.5f, ForceMode.Impulse); //revert numbers 
                    ballrigid.AddForce(direction * 50f, ForceMode.Impulse);

                    isservesequence = false;
                    hitAIrecently = true;
                    bounces = 0;
                    gameObject.GetComponent<Positiondetection>().whohitlast = false;
                    gameObject.GetComponent<Positiondetection>().bounced = false;
                    StartCoroutine(ballhitDelayRoutine());

                }
                else
                {
                    Debug.Log("Hit the ball outside of serve");
                    this.gameObject.GetComponent<Positiondetection>().playerhitlast();
                    this.gameObject.GetComponent<Positiondetection>().setbouncedfalse();
                    // Solution 1
                    // direction = (transform.position - coll.transform.position).normalized;
                    direction = coll.gameObject.GetComponent<controllervelocity>().getvelocity();
                    //ballrigid.velocity = new Vector3(0, 0, 0);
                    ballrigid.AddForce(direction * 40f, ForceMode.Impulse);
                    //ballrigid.AddForce(direction * 5.5f, ForceMode.Impulse); //revert numbers 
                    hitAIrecently = true;
                    bounces = 0;
                    gameObject.GetComponent<Positiondetection>().whohitlast = false;
                    gameObject.GetComponent<Positiondetection>().bounced = false;
                    StartCoroutine(ballhitDelayRoutine());
                }
            }
        }


    }
    public void setservesequence()
    {
        isservesequence = true;
    }
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

    }
    IEnumerator ballhitDelayRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        hitAIrecently = false;
    }
    IEnumerator ballhitgroundDelayRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        hitgroundtoorecently = false;
    }

}

