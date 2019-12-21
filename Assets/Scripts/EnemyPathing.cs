using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Configuration
    [SerializeField] float moveSpeed = 2f;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Cached References
    [SerializeField] WaveConfig waveConfig;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position; // Sets position of enemy to position of indexed waypoint.
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnPath();
    }

    // Moves gameObject along a defined path.
    void MoveOnPath()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector3 targetPosition = waypoints[waypointIndex].transform.position; // Stores location of next waypoint position.
            float step = moveSpeed * Time.deltaTime; // Stores speed of movement.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step); // Moves gameObject position towards position of waypoint.

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject); // Destorys gameObject when it has reached the last waypoint position.
        }
    }
}
