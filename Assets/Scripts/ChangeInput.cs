using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
                Debug.Log("REVERSE REVERSE");
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
                Debug.Log("RIGHT FOOT NOW");
            }
        }
    }
}
