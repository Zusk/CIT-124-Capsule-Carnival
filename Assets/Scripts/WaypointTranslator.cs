using UnityEngine;
using System.Collections;

public class WaypointTranslator : MonoBehaviour
{

    public Transform[] waypoints;
    public float moveSpeed;
    public float rotationSpeed;

    private int currentWaypoint = 0;

    void Update()
    {
        //check if we have reached the target waypoint
        if (transform.position == waypoints[currentWaypoint].position)
        {
            //if we have reached the last waypoint in the array, reset back to the first waypoint
            if (currentWaypoint == waypoints.Length - 1)
            {
                currentWaypoint = 0;
            }
            //if we have not reached the last waypoint in the array, continue to the next waypoint
            else
            {
                currentWaypoint++;
            }
        }

        //move towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);
    }

}