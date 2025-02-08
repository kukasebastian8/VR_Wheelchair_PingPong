using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NavigateOnEnter : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var currentGameObject = GameObject.Find("Main Menu");
            gameObject.SetActive(false);

            var nextGameObject = GameObject.Find("Play Menu");
            nextGameObject.SetActive(true);
        }
    }
}
