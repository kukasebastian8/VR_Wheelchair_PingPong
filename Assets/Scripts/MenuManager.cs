using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject tutorialMenuCanvas; // Reference to Tutorial Menu Canvas
    public GameObject statisticsMenuCanvas; // Reference to Statistics Menu Canvas

    void Start()
    {
        // Show only the Tutorial Menu at the start
        tutorialMenuCanvas.SetActive(true);
        statisticsMenuCanvas.SetActive(false);
    }

    public void OpenStatisticsMenu()
    {
        // Hide Tutorial Menu and show Statistics Menu
        tutorialMenuCanvas.SetActive(false);
        statisticsMenuCanvas.SetActive(true);
    }

    public void OpenTutorialMenu()
    {
        // Hide Statistics Menu and show Tutorial Menu
        statisticsMenuCanvas.SetActive(false);
        tutorialMenuCanvas.SetActive(true);
    }
}