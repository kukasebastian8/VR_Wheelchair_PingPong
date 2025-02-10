using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballhitignore : MonoBehaviour
{
    public Collider thisbox;
    private GameObject ball;
    private Collider ballCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ball = GameObject.Find("Ball(Clone)");
        if (ball != null)
        {
           
            ballCollider = ball.GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(thisbox, ballCollider, true);
        }
        }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("tag of object: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ball") {
            Debug.Log("Ball should start ignoring collisio with this");
            Physics.IgnoreCollision(thisbox, collision.gameObject.GetComponent<Collider>(), true);

        }
    }


}
