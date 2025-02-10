using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qudrantsetter : MonoBehaviour
{
    public int quadrant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BallFinal") {
            other.GetComponentInParent<Positiondetection>().setquad(quadrant);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
