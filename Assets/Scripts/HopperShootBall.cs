using UnityEngine;


public class HopperShootBall : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform[] gunPoints;
    public float fireRate;
    public Transform turretBase;

    bool firing;
    float fireTimer;
    public float speed = 15;

    int gunPointIndex;

    void Update()
    {
        if (firing)
        {
            while (fireTimer >= 1 / fireRate)
            {
                SpawnShot();
                fireTimer -= 1 / fireRate;
            }

            fireTimer += Time.deltaTime;
            firing = false;
        }
        else
        {
            if (fireTimer < 1 / fireRate)
            {
                fireTimer += Time.deltaTime;
            }
            else
            {
                fireTimer = 1 / fireRate;
            }
        }
    }

    void SpawnShot()
    {
        var gunPoint = gunPoints[gunPointIndex++];
        GameObject ball = Instantiate(shotPrefab, gunPoint.position, gunPoint.rotation);
        gunPointIndex %= gunPoints.Length;
        ball.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    public void Fire()
    {
        firing = true;
    }
}