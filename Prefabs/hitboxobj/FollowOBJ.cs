using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOBJ : MonoBehaviour
{
    public GameObject objtofollow;
    public int sensitivity = 5;
    Vector3 destination;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        destination = objtofollow.transform.position - this.transform.position;
        transform.rotation = objtofollow.transform.rotation;
        rb.velocity = destination*sensitivity; //- this.transform.position;// * sensitivity ;


    }
}
