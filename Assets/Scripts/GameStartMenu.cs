using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button tutorialButton;
    public Button twoVStwoButton;
    public Button oneVSoneButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        oneVSoneButton.onClick.AddListener(Start1V1Game);
        twoVStwoButton.onClick.AddListener(Start2V2Game);
        tutorialButton.onClick.AddListener(StartTutorial);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start1V1Game()
    {
        string startingScene = "AI_TESTSCENE_COPY";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(startingScene);
    }
    public void Start2V2Game()
    {
        string startingScene = "AI2v2";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(startingScene);
    }

    public void StartTutorial()
    {
        string tutorialScene = "PlayScene - Copy";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(tutorialScene);
    }


    public void HideAll()
    {
        mainMenu.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        about.SetActive(false);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        about.SetActive(true);
    }
}
