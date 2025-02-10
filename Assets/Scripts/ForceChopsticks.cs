using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceChopsticks : MonoBehaviour
{
    public GameObject leftControllerInteractor;
    public GameObject rightControllerInteractor;
    public bool isactive;

    // Start is called before the first frame update
    void Awake()
    {
        if (leftControllerInteractor != null)
        {
            leftControllerInteractor.SetActive(isactive);
        }
        if (rightControllerInteractor != null)
        {
            rightControllerInteractor.SetActive(isactive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
