using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PicklePaddleController : MonoBehaviour
{
    public GameObject picklePaddle;
    public InputActionReference selectActionReference;

    private InputAction selectAction;

    private void Start()
    {
        // Initially set the paddle to be inactive
        picklePaddle.SetActive(false);

        // Get the input action from the reference
        selectAction = selectActionReference.action;

        // Register the action callbacks
        selectAction.performed += OnSelectPerformed;
        selectAction.canceled += OnSelectCanceled;

        // Enable the action
        selectAction.Enable();
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        // Activate the paddle when the selection button is pressed
        picklePaddle.SetActive(true);
    }

    private void OnSelectCanceled(InputAction.CallbackContext context)
    {
        // Deactivate the paddle when the selection button is released
        picklePaddle.SetActive(false);
    }

    private void OnDestroy()
    {
        // Unregister the action callbacks
        selectAction.performed -= OnSelectPerformed;
        selectAction.canceled -= OnSelectCanceled;
    }
}
