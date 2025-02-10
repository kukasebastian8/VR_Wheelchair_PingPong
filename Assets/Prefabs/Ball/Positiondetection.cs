using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Positiondetection : MonoBehaviour
{
    public bool playingmatch = false;
    public int temp = 0;
    public int ballboundschecker = 0;
    public GameObject court;
    public bool teamserve = false; // false if players team true for enemy team
    public bool whohitlast = false; // false if players team true for enemy team
    Vector3 courtposition;
    public GameObject scoreboard;
    public bool bounced = false;
    public bool turretball = false;
    private int firstquadant = -1;
    private int currquadant;
    public bool webounedonceever = false;
    private bool wehitnet = false;
    private int tableBounceCount = 0;

    private bool isServeInProgress = false; // Track serve status
    private int serveBounceCount = 0; // Track serve bounces

    void Start()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");
        if (scoreboard.GetComponent<ScoreboardScript>().getserve() == 2 || scoreboard.GetComponent<ScoreboardScript>().getserve() == 3)
        {
            teamserve = true;
        }
        else
        {
            teamserve = false;
        }

        court = GameObject.FindGameObjectWithTag("Court");
        courtposition = court.transform.position;
    }

    void Update()
    {
        checkundermap();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainTable"))
        {
            if (isServeInProgress)
            {
                serveBounceCount++;

                if (serveBounceCount > 1)
                {
                    EndServe();
                    changescoreboard();
                    ReloadScene();
                    return;
                }
            }
            else
            {
                tableBounceCount++;

                if (tableBounceCount >= 2)
                {
                    changescoreboard();
                    ReloadScene();
                    return;
                }
            }

            bounced = true;
            return;
        }
        else if (collision.gameObject.name == "Court")
        {
            if (!webounedonceever)
            {
                if (firstquadant < 3)
                {
                    if (!(currquadant == (firstquadant + 2)) && currquadant > 2)
                    {
                        Debug.Log("QWERTYUIOPASDFGxxxx");
                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                        ReloadScene();
                    }
                }
                else
                {
                    if (!(currquadant == (firstquadant - 2)) && currquadant < 3)
                    {
                        Debug.Log("QWERTYUIOPASDFGxxxxx");
                        scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
                        ReloadScene();
                    }
                }
            }
            webounedonceever = true;
            bounced = true;
        }
        else if (collision.gameObject.CompareTag("CourtPart"))
        {
            temp = 1;
            if (isinboundszaxis())
            {
                ballboundschecker = checkboundsx();
                if (ballboundschecker == 0)
                    return;
                if (ballboundschecker == -1)
                {
                    HandleOutOfBounds(true);
                    return;
                }
                if (ballboundschecker == 1)
                {
                    HandleOutOfBounds(false);
                    return;
                }
            }
        }
        else if (collision.gameObject.CompareTag("NET"))
        {
            changescoreboard();
            ReloadScene();
        }
    }

    private void HandleOutOfBounds(bool isOpponentSide)
    {
        if (turretball)
        {
            Destroy(gameObject);
            return;
        }
        if (playingmatch)
        {
            if (isOpponentSide)
            {
                scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
            }
            else
            {
                scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
            }
            ReloadScene();
        }
    }

    private void changescoreboard()
    {
        if (!whohitlast && !teamserve)
        {
            Debug.Log("QWERTYU");
            scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
        }
        else if (whohitlast && !teamserve)
        {
            Debug.Log("QWERTYU Scored player ");
            scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
        }
        else if (whohitlast && teamserve)
        {
            Debug.Log("QWERTYUI");
            scoreboard.GetComponent<ScoreboardScript>().ChangeServing();
        }
        else if (!whohitlast && teamserve)
        {
            Debug.Log("QWERTYU Scored Ai ");
            scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
        }
    }

    private void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void StartServe()
    {
        isServeInProgress = true;
        serveBounceCount = 0;
    }

    public void EndServe()
    {
        isServeInProgress = false;
        serveBounceCount = 0;
    }

    public bool isinboundszaxis()
    {
        if (transform.position.z > courtposition.z + 3)
            return false;
        if (transform.position.z < courtposition.z - 3)
            return false;
        return true;
    }

    public int checkboundsx()
    {
        if (transform.position.x > courtposition.x + 6.8)
            return -1;
        if (transform.position.x < courtposition.x - 6.6)
            return 1;
        return 0;
    }

    public void setplaying()
    {
        playingmatch = true;
    }

    public void playerhitlast()
    {
        whohitlast = false;
    }

    public void AIhistlast()
    {
        whohitlast = true;
    }

    public void setbouncedfalse()
    {
        bounced = false;
    }

    private void checkundermap()
    {
        int temp = checkboundsx();
        if (this.transform.position.y < -1)
        {
            if (turretball)
            {
                Destroy(gameObject);
                return;
            }
            changescoreboard();
            ReloadScene();
        }
    }

    public void setquad(int quadrant)
    {
        if (firstquadant == -1)
        {
            firstquadant = quadrant;
        }
        currquadant = quadrant;
    }
}

