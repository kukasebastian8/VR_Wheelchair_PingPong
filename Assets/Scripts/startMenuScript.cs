
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenuScript : MonoBehaviour
{
    [Header("UI Containers")]
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject Controls;
    public GameObject GameSelect;

    [Header("Start UI Buttons")]
    public Button BeginButton;
    public Button ControlsButton;
    public Button CreditsButton;
    public Button QuitButton;

    [Header("Game Selection UI Buttons")]
    public Button TrainingButton;
    public Button OneVOneButton;
    public Button TwoVTwoButton;
    public Button GameSelectReturnButton;

    [Header("Control UI Buttons")]
    public Button ControlReturnButton;
    public Button LeftHandButton;
    public Button RightHandButton;
    public GameObject PlayerSettings;
    private PlayerSettings playerSettings;

    [Header("Credits UI Buttons")]
    public Button CreditsReturnButton; 

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        Controls.SetActive(false);
        GameSelect.SetActive(false);
        playerSettings = GameObject.Find("Settings Object").GetComponent<PlayerSettings>();
    }
    #region Main Menu
    public void OpenGameSelect()
    {
        MainMenu.SetActive(false);
        GameSelect.SetActive(true);
    }
    public void OpenControls()
    {
        MainMenu.SetActive(false);
        Controls.SetActive(true);
    }
    public void OpenCredits()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Game Select
    public void StartTraining()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void StartSingles()
    {
        SceneManager.LoadScene("AI_TESTSCENE");
    }
    public void StartDoubles()
    {
        SceneManager.LoadScene("AI2v2");
    }
    public void CloseGameSelect()
    {
        MainMenu.SetActive(true);
        GameSelect.SetActive(false);
    }
    #endregion

    #region Controls
    public void CloseControls()
    {
        MainMenu.SetActive(true);
        Controls.SetActive(false);
    }

    public void SelectLeft()
    {
        if (playerSettings != null)
        {
            playerSettings.isRightHand = false;
        }

    }

    public void SelectRight()
    {
        if (playerSettings != null)
        {
            playerSettings.isRightHand = true;
        }
    }

    #endregion
    public void CloseCredits()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
    }



}
