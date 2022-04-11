using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] pointsFromStart;
    public Vector3[] points;
    public int pointNumber = 0;
    private Vector3 currentTarget;

    public float tolerance;
    public float speed; // how fast the platform moves
    public float delayTimer; // how long untill the platform moves after stoping

    private float delayStart;

    // should the platform move on its own
    public bool automatic;

    void Start()
    {
        FixPoints();
        if (points.Length > 0)
        {
            currentTarget = points[0];
        }
        tolerance = speed * Time.deltaTime;
    }
    void FixPoints()
    {
        points = new Vector3[pointsFromStart.Length + 1];
        points[0] = transform.position;
        for(int i = 1; i <= pointsFromStart.Length; i++)
        {
            points[i] = transform.position + pointsFromStart[i - 1];
        }
    }

    void FixedUpdate()
    {
        if (transform.position != currentTarget)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    // moves to the next platfrom
    void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    // automatically moves to next platform
    void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delayStart > delayTimer)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        pointNumber++;
        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }
        currentTarget = points[pointNumber];
    }

    // makes the player stick to the platfrom
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
