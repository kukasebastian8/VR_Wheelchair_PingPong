using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoremIrror : MonoBehaviour
{
    public int playerScore = 0;
    public int aiScore = 0;
    public TextMeshPro playerScoreboard;
    public TextMeshPro aiScoreboard;
    public TextMeshPro serverScoreboard;
    public TextMeshPro ServeIndicatorL;
    public TextMeshPro ServeIndicatorR;
    public int server = 0;
    public bool isDoubles;
    GameObject scoreboard;
    ScoreboardScript SBS;
    // Start is called before the first frame update
    void Start()
    {
        scoreboard= GameObject.FindWithTag("Scoreboard");
        SBS = scoreboard.GetComponent<ScoreboardScript>();
        playerScore = SBS.playerScore;
        aiScore = SBS.aiScore;
        server = SBS.server;
        isDoubles = SBS.isDoubles;
        UpdateScoreboard();


    }

    // Update is called once per frame
    void Update()
    {
        
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


}
