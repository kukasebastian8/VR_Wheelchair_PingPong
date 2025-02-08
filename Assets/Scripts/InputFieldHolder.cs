using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldHolder : MonoBehaviour
{
    public TextMeshProUGUI userName;
    public TMP_InputField user_inputField;

    public void ReadInputFromUser()
    {
        userName.text = user_inputField.text;
    }
}
