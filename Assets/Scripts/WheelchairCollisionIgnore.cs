using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairCollisionIgnore : MonoBehaviour
{
    private GameObject ball;
    private Collider ballCollider;
    public Collider Left_Wheel;
    public Collider Right_Wheel;
    public Collider Left_Caster;
    public Collider Right_Caster;
    public Collider Teammate1;
    public Collider Teammate2;
    private void Awake()
    {
        if (Teammate1) {
            Physics.IgnoreCollision(Left_Wheel, Teammate1, true);
            Physics.IgnoreCollision(Right_Wheel, Teammate1, true);
            Physics.IgnoreCollision(Left_Caster, Teammate1, true);
            Physics.IgnoreCollision(Right_Caster, Teammate1, true);
        }
        if (Teammate2) {
            Physics.IgnoreCollision(Left_Wheel, Teammate2, true);
            Physics.IgnoreCollision(Right_Wheel, Teammate2, true);
            Physics.IgnoreCollision(Left_Caster, Teammate2, true);
            Physics.IgnoreCollision(Right_Caster, Teammate2, true);
        }
    }
    void Update()
    {
        ball = GameObject.Find("Ball(Clone)");
        if (ball != null)
        {
            // Log to confirm that the ball is found
            //ebug.Log("BALL FOUND");

            // Find the BallFinal child object and get its collider
            Transform ballFinalTransform = ball.transform.Find("BallFinal");

            // Check if BallFinal exists
            if (ballFinalTransform != null)
            {
                ballCollider = ballFinalTransform.GetComponent<Collider>();
                if (ballCollider != null)
                {
                    // Ignore collision for each of the wheelchair's colliders
                    Physics.IgnoreCollision(Left_Wheel, ballCollider, true);
                    Physics.IgnoreCollision(Right_Wheel, ballCollider, true);
                    Physics.IgnoreCollision(Left_Caster, ballCollider, true);
                    Physics.IgnoreCollision(Right_Caster, ballCollider, true);
                }
                else
                {
                    Debug.LogWarning("BallFinal does not have a collider attached.");
                }
            }
            else
            {
                Debug.LogWarning("BallFinal child not found inside Ball(Clone).");
            }
        }
    }
}
