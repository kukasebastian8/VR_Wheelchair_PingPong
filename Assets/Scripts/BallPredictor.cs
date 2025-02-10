using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


[RequireComponent(typeof(LineRenderer))]
public class BallPredictor : MonoBehaviour
{
    #region Members
    LineRenderer trajectoryLine;

    [SerializeField, Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")]
    int maxPoints = 50;

    [SerializeField, Range(0.01f, 0.5f), Tooltip("The time increment used to calculate the trajectory")]
    float increment = 0.025f;

    [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
    float rayOverlap = 1.1f;
    #endregion

    private Rigidbody ballrigbody;
    public Vector3 endPoint;

    private void Start()
    {
        if (trajectoryLine == null)
            trajectoryLine = GetComponent<LineRenderer>();
        ballrigbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       PredictTrajectory();
    }

    public void PredictTrajectory()
    {
        Vector3 velocity = ballrigbody.velocity;
        Vector3 position = transform.position;
        Vector3 nextPosition;
        float overlap;

        UpdateLineRender(maxPoints, (0, position));

        for (int i = 1; i < maxPoints; i++)
        {
            // Estimate velocity and update next predicted position
            velocity = CalculateNewVelocity(velocity, ballrigbody.angularDrag, increment);
            nextPosition = position + velocity * increment;

            // Overlap our rays by small margin to ensure we never miss a surface
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            //When hitting a surface we want to show the surface marker and stop updating our line
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                endPoint = hit.point;
                UpdateLineRender(i, (i - 1, hit.point));
                break;
            }

            //If nothing is hit, continue rendering the arc without a visual marker
            position = nextPosition;
            UpdateLineRender(maxPoints, (i, position)); //Unneccesary to set count here, but not harmful
        }
    }

    /// <summary>
    /// Allows us to set line count and an induvidual position at the same time
    /// </summary>
    /// <param name="count">Number of points in our line</param>
    /// <param name="pointPos">The position of an induvidual point</param>
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        trajectoryLine.positionCount = count;
        trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }
}