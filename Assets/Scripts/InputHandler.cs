using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField UsernameInputField; // Reference to the username input field
    public TextMeshProUGUI WelcomeText; // Reference to the welcome message text

    public void UpdateWelcomeMessage()
    {
        // Get the username from the input field
        string username = UsernameInputField.text;

        // Update the welcome message
        if (!string.IsNullOrEmpty(username))
        {
            WelcomeText.text = $"Welcome, {username}!";
        }
        else
        {
            WelcomeText.text = "Welcome, Player!";
        }
    }
}