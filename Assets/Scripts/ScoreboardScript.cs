
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreboardScript : MonoBehaviour
{
    private static ScoreboardScript instance;
    public int playerScore = 0;
    public int aiScore = 0;
    public int server = 0;
    public bool isDoubles;
    private int previousscoreAI = 0;
    private int previousscoreplayer = 0;
    public TextMeshPro playerScoreboard;
    public TextMeshPro aiScoreboard;
    public TextMeshPro serverScoreboard;
    public TextMeshPro ServeIndicatorL;
    public TextMeshPro ServeIndicatorR;

    public string currentSceneName;

    /*private void Awake()
    {
        // Check if there's already an instance of ScoreboardScript
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateScoreboard();
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currentSceneName = SceneManager.GetActiveScene().name; // Add this line
            UpdateScoreboard();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (aiScore >= 11) {
            if (aiScore - playerScore >= 2) {
                SceneManager.LoadSceneAsync("Lose Scene");
                SceneManager.LoadScene("Lose Scene");
            }
        
        }
        if (playerScore >= 11)
        {
            if ( playerScore -aiScore >= 2)
            {
                SceneManager.LoadSceneAsync("Win Scene");
                SceneManager.LoadScene("Win Scene");
            }

        }
        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != currentSceneName)
        {
            Debug.Log(SceneManager.GetActiveScene().name + "\n" + currentSceneName);
            Debug.Log("Incorrect Scene Located. Removing Scoreboard.");
            Destroy(gameObject);
        }

        //Change Scene to see if deletion works
        /*if (Input.GetKeyDown("b"))
        {
            SceneManager.LoadScene("startScene");
        }

        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene(currentScene.name);   
        }

        if (Input.GetKeyDown("o"))
        {
            IncreaseScore(true);
        }

        if (Input.GetKeyDown("p"))
        {
            IncreaseScore(false);
        }

        if (Input.GetKeyDown("i"))
        {
            ChangeServing();
        }*/
        // Update your scoreboard logic here if necessary
    }

    void UpdateScoreboard()
    {
        playerScoreboard.text = playerScore.ToString();
        aiScoreboard.text = aiScore.ToString();
        if (isDoubles)
        {
            serverScoreboard.gameObject.SetActive(true);
            if (server < 2)
            {
                ServeIndicatorL.gameObject.SetActive(true);
                ServeIndicatorR.gameObject.SetActive(false);
            }
            else
            {
                ServeIndicatorL.gameObject.SetActive(false);
                ServeIndicatorR.gameObject.SetActive(true);
            }

            serverScoreboard.text = ((server % 2) + 1).ToString(); 
        }

        else
        {
            serverScoreboard.gameObject.SetActive(false);
            if (server < 1)
            {
                ServeIndicatorL.gameObject.SetActive(true);
                ServeIndicatorR.gameObject.SetActive(false);
            }
            else
            {
                ServeIndicatorL.gameObject.SetActive(false);
                ServeIndicatorR.gameObject.SetActive(true);
            }

            serverScoreboard.text = ((server % 2) + 1).ToString();

        }
        
    }

    public void IncreaseScore(bool isPlayer)
    {
        if (isPlayer) {playerScore += 1;}
        else {aiScore += 1;}
        UpdateScoreboard();
    }

    public void ChangeServing()
    {
        if (isDoubles) {server = (server + 1) % 4;}
        else { server = (server + 2) % 4; }
        UpdateScoreboard();
    }
    public int getserve() {
        return server;
    
    }
    void OnSceneLoaded()
    {
        if ((previousscoreAI + 1) >= aiScore)
        {

        }
        else {
            aiScore = previousscoreAI + 1;
        }
        previousscoreAI = aiScore;
        if ((previousscoreplayer + 1) >= playerScore)
        {

        }
        else
        {
            playerScore = previousscoreplayer + 1;
        }
        previousscoreplayer = playerScore;


    }
    public int getplayerScore() {
        return playerScore;
    }
    public int getaiScore()
    {
        return aiScore;
    }
}