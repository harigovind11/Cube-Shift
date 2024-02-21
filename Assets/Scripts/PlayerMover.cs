using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 20f;
    public float rotationSpeed = 5f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            // transform.LookAt(targetPosition, Vector3.up);
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            transform.position = transform.forward * speed * Time.deltaTime;
            // if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            // {
            //     Vector3 direction = (targetPosition - transform.position).normalized;
            //     Quaternion lookRotation = Quaternion.LookRotation(direction);
            //     // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            //     transform.rotation = lookRotation;
            // }

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                currentWaypointIndex++;
            }
            // if (transform.position == targetPosition)
            // {
            //     currentWaypointIndex++;
            // }
        }
    }
}