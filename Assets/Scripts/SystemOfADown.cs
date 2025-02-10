using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemOfADown : MonoBehaviour
{
    public GameObject scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log("Fuck my stupud chungus life please die");
        scoreboard.SetActive(false);
        scoreboard.SetActive(true);
    }
}
