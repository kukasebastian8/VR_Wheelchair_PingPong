using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballstart : MonoBehaviour
{
    public Rigidbody rigidenabler;
    public SphereCollider enablebounce;
    private PhysicMaterial tempmaterial;
    public bool isenabled =true;
    // Start is called before the first frame update
    void Start()
    {

        if (isenabled)
            return;
        rigidenabler.useGravity = false;
        tempmaterial = enablebounce.material;
        enablebounce.material = null;




    }
    void OnCollisionEnter(Collision collision)
    {
        if (isenabled)
        return;

        enablebounce.material = tempmaterial;
        rigidenabler.useGravity = true;
        isenabled = true;



    }

        // Update is called once per frame
        void Update()
    {
       
    }
}
