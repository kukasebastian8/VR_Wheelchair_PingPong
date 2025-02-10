using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Predictor))]
public class PickleShoot : MonoBehaviour
{
    Predictor predict;

    [SerializeField]
    Rigidbody ballPrefab;

    [SerializeField, Range(0.0f, max:50.0f)]
    float force;

    [SerializeField]
    Transform StartPosition;

    public float areaSize;
    [Tooltip("Start time is how to stagger the shots")]
    public int fireStartTime = 0;
    float fireElapsedTime = 0;
    [Tooltip("Delays should be the same for all ponts of shooting")]
    public float fireDelay = 8f;

    void Start()
    {
        fireElapsedTime = fireStartTime;
        predict = GetComponent<Predictor>();

        if (StartPosition == null)
            StartPosition = transform;
        
    }

    void Update()
    {
        Predict();
        fireElapsedTime += Time.deltaTime;

        if(fireElapsedTime >= fireDelay)
        {
            HitBall();
            fireElapsedTime = 0;
        }
    }

    void Predict()
    {
        predict.PredictTrajectory(HitData());
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

    void HitBall()
    {
        Rigidbody hitBall =  Instantiate(ballPrefab, StartPosition.position, Quaternion.identity);
        hitBall.GetComponentInParent<ballcollision>().turretball =true;
        hitBall.GetComponentInParent<ballcollision>().bouncedplayer = true;
        hitBall.GetComponentInParent<Positiondetection>().turretball = true;
        hitBall.AddForce(-StartPosition.right * force, ForceMode.Impulse);
    }

}
