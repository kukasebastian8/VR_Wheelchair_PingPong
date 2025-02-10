using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHasBeenHit : MonoBehaviour
{
    BallPredictor predict;

    [SerializeField]
    Rigidbody ballPrefab;

    [SerializeField, Range(0.0f, max:50.0f)]
    float force;

    [SerializeField]
    Transform StartPosition;

    public InputAction action;

    void OnEnable()
    {
        predict = GetComponent<BallPredictor>();

        if (StartPosition == null)
            StartPosition = transform;
        
        action.Enable();
        action.performed += HitBall;
    }

    void Update()
    {
        Predict();
    }

    void Predict()
    {
        predict.PredictTrajectory();
    }

    BallProperties HitData()
    {
        BallProperties properties = new BallProperties();
        Rigidbody ball = ballPrefab.GetComponent<Rigidbody>();

        properties.direction = -StartPosition.right;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = ball.mass;
        properties.windDrag = ball.drag;

        return properties;
    }

    void HitBall(InputAction.CallbackContext ctx)
    {
        Rigidbody hitBall = Instantiate(ballPrefab, StartPosition.position, Quaternion.identity);
        hitBall.AddForce(-StartPosition.right * force, ForceMode.Impulse);
    }

}
