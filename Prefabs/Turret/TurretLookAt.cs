using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLookAt : MonoBehaviour
{
    Vector3 tempVector;
    public Transform target;


    // Update is called once per frame
    void Update()
    {
         tempVector = new Vector3(target.position.x, target.position.y - target.position.x, target.position.z);
        transform.LookAt(tempVector);
    }
}
