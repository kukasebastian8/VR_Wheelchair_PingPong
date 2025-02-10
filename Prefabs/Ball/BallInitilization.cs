using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initilization : MonoBehaviour
{
    public float gravitymult = -.9f;
    private Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.AddForce(Physics.gravity * gravitymult);
    }
}
