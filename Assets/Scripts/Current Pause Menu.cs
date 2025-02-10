using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class TEMPPause : MonoBehaviour
{
    private bool OpendPauseMenuTooRecently= false;
    public GameObject pauseMenu;
    private bool isPaused = false;
    public GameObject canvasFlip;

    public GameObject leftControllerInteractor;
    public GameObject rightControllerInteractor;

    private List<InputDevice> inputDevices = new List<InputDevice>();

    private void Start()
    {
        pauseMenu.SetActive(false);
        canvasFlip.transform.Rotate(0.0f, 180.0f, 0.0f);
        //SetControllerInteractorsActive(false);

        // Retrieve devices at startup
        RetrieveInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected; // Register for new devices
        InputDevices.deviceDisconnected += OnDeviceDisconnected; // Handle disconnections
    }
    IEnumerator buttonpressdelay()
    {
        // Will wait .5 seconds then allow dor the pause button to be pressed again. Should work when the game is paused. 
        yield return new WaitForSecondsRealtime(0.5f);
        OpendPauseMenuTooRecently = false;
    }
    private void RetrieveInputDevices()
    {
        InputDevices.GetDevices(inputDevices);
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (!inputDevices.Contains(device))
        {
            inputDevices.Add(device);
        }
    }

    private void OnDeviceDisconnected(InputDevice device)
    {
        if (inputDevices.Contains(device))
        {
            inputDevices.Remove(device);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            FollowPlayer();
        }

        // checks to see if the pause button can be pressed 
        if (!OpendPauseMenuTooRecently)
        {
            // Check the menu button on all connected devices
            foreach (var device in inputDevices)
            {
                if (device.TryGetFeatureValue(CommonUsages.menuButton, out bool isMenuPressed) && isMenuPressed)
                {
                    FlipPauseState(isPaused);
                    // prvent the pause button from being pressed
                    OpendPauseMenuTooRecently = true;
                    StartCoroutine(buttonpressdelay());
                    break; // Avoid multiple flips in the same frame
                }
            }
        }
        // Fallback for testing with a keyboard
        if (Input.GetKeyDown(KeyCode.C))
        {
            FlipPauseState(isPaused);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        SetControllerInteractorsActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        SetControllerInteractorsActive(true);
    }

    public void QuitGame()
    {
        FlipPauseState(isPaused);
        Debug.Log("CLOSING GAME");
        Application.Quit();
    }

    public void MainMenu()
    {
        FlipPauseState(isPaused);
        SceneManager.LoadSceneAsync(0);
    }

    public void FlipPauseState(bool isPaused)
    {
        if (isPaused) { ResumeGame(); }
        else { PauseGame(); }
    }

    public void FollowPlayer()
    {
        Transform playerCam = Camera.main.transform;
        pauseMenu.transform.position = playerCam.position + playerCam.forward;

        // Set the Y rotation of the menu to 180 degrees
        pauseMenu.transform.rotation = Quaternion.Euler(
            pauseMenu.transform.rotation.eulerAngles.x,
            pauseMenu.transform.rotation.eulerAngles.y + 180,
            pauseMenu.transform.rotation.eulerAngles.z
        );
        pauseMenu.transform.LookAt(playerCam.position);
    }

    private void SetControllerInteractorsActive(bool isActive)
    {
        if (leftControllerInteractor != null)
        {
            leftControllerInteractor.SetActive(isActive);
        }
        if (rightControllerInteractor != null)
        {
            rightControllerInteractor.SetActive(isActive);
        }
    }
}
