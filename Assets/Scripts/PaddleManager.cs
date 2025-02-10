using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleManager : MonoBehaviour
{
    private GameObject spawnedball;
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject AI1;
    public GameObject AI2;
    public GameObject AI3;
    public GameObject Ball;
    public InputActionReference leftSelectActionReference;
    public InputActionReference rightSelectActionReference;
    public bool servesequence = true;
    private InputAction leftSelectAction;
    private InputAction rightSelectAction;
    public bool turretmode =false;
    private static bool isLeftPaddleActive = false;
    private static bool isRightPaddleActive = false;
    public GameObject scoreboard;
    private bool isdoubles = false;
    private void Start()
    {
        if (!AI1) { 
        AI1 = GameObject.Find("AI2v2 t2");

        }
        scoreboard = GameObject.FindWithTag("Scoreboard");
        if (scoreboard.GetComponent<ScoreboardScript>().getserve() == 0)
        {
            servesequence = true;
        }
        else
        {
            servesequence = false;
        }
        if (scoreboard.GetComponent<ScoreboardScript>().isDoubles)
        { 
        isdoubles = true;
        }
        // Initially set the paddles to be inactive
        leftPaddle.SetActive(false);
        rightPaddle.SetActive(false);

        // Get the input actions from the references
        leftSelectAction = leftSelectActionReference.action;
        rightSelectAction = rightSelectActionReference.action;

        // Register the action callbacks
        leftSelectAction.performed += OnLeftSelectPerformed;
        leftSelectAction.canceled += OnLeftSelectCanceled;
        rightSelectAction.performed += OnRightSelectPerformed;
        rightSelectAction.canceled += OnRightSelectCanceled;

        // Enable the actions
        leftSelectAction.Enable();
        rightSelectAction.Enable();
    }

    private void OnLeftSelectPerformed(InputAction.CallbackContext context)
    {
        if (!isRightPaddleActive)
        {
            leftPaddle.SetActive(true);
            isLeftPaddleActive = true;
        }
        else if (servesequence)
        {

            spawnedball = Instantiate(Ball, leftPaddle.transform.position, Quaternion.identity);
            spawnedball.GetComponent<ballcollision>().setservesequence();
            if (turretmode) {
                spawnedball.GetComponentInParent<ballcollision>().turretball = true;
                spawnedball.GetComponentInParent<ballcollision>().bouncedplayer = true;
                spawnedball.GetComponentInParent<Positiondetection>().turretball = true;

            }
            else
            {
                spawnedball.GetComponent<Positiondetection>().setplaying();
            
                servesequence = false;
                if (!isdoubles)
                {
                    AI1.gameObject.GetComponent<positiontargeter>().target = spawnedball;
                }
                else
                {
                    if (!AI1) {

                        AI1 = GameObject.Find("AI2v2 t2");
                    }
                    AI1.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;
                    AI2.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;
                    AI3.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;


                }
            }
        }
    }

    private void OnLeftSelectCanceled(InputAction.CallbackContext context)
    {
        leftPaddle.SetActive(false);
        isLeftPaddleActive = false;
    }

    private void OnRightSelectPerformed(InputAction.CallbackContext context)
    {
        if (!isLeftPaddleActive)
        {
            rightPaddle.SetActive(true);
            isRightPaddleActive = true;
        }
        else if (servesequence) {

            spawnedball = Instantiate(Ball, rightPaddle.transform.position, Quaternion.identity);
            spawnedball.GetComponent<ballcollision>().setservesequence();
            if (turretmode)
            {
                spawnedball.GetComponentInParent<ballcollision>().turretball = true;
                spawnedball.GetComponentInParent<ballcollision>().bouncedplayer = true;
                spawnedball.GetComponentInParent<Positiondetection>().turretball = true;

            }
            else
            {
                spawnedball.GetComponent<Positiondetection>().setplaying();
                servesequence = false;
                if (!isdoubles)
                {
                    AI1.gameObject.GetComponent<positiontargeter>().target = spawnedball;
                }
                else
                {
                    if (!AI1)
                    {

                        AI1 = GameObject.Find("AI2v2 t2");
                    }
                    AI1.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;
                    AI2.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;
                    AI3.gameObject.GetComponent<_2v2Positiontargeter>().target = spawnedball;


                }
            }
        }

        
    }

    private void OnRightSelectCanceled(InputAction.CallbackContext context)
    {
        rightPaddle.SetActive(false);
        isRightPaddleActive = false;
    }
    
    private void OnDestroy()
    {
        // Unregister the action callbacks
        leftSelectAction.performed -= OnLeftSelectPerformed;
        leftSelectAction.canceled -= OnLeftSelectCanceled;
        rightSelectAction.performed -= OnRightSelectPerformed;
        rightSelectAction.canceled -= OnRightSelectCanceled;
    }
}
