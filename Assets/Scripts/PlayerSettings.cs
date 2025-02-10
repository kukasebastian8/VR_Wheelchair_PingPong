using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private static PlayerSettings instance;

    public bool isRightHand;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
    private void Awake()
    {
        // Check if there's already an instance of ScoreboardScript
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
