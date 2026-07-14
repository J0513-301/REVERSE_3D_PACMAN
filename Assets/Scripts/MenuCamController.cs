using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public Transform[] waypoints;

    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;

    public float waitTime = 2f;

    private int currentPoint = 0;
    private float waitTimer = 0f;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform target = waypoints[currentPoint];

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime);

        // Rotate toward waypoint rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime);

        // Arrived?
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;

                currentPoint++;

                if (currentPoint >= waypoints.Length)
                    currentPoint = 0;
            }
        }
    }
}