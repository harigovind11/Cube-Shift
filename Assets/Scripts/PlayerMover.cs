using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Transform[] waypoints;
    private int collisionMoveBackDistance = 2;
    private int currentWaypointIndex = 0;

    public float moveSpeed = 15f; // Movement speed towards the waypoint
    public float turnSpeed = 10f; // Rotation speed to look at the waypoint
    public float reachThreshold = 0.1f; // Distance threshold to consider the waypoint reached

    private bool isMoving = true;

    public float collisionPauseDuration = 1f;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveTowardsWaypoint();
        }
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            RotateTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Length == 0) return; // Exit if no waypoints are set

        // Get the current waypoint target
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 directionToWaypoint = targetWaypoint.position - transform.position;
        Vector3 moveVector = directionToWaypoint.normalized * moveSpeed * Time.fixedDeltaTime;

        // Move towards the waypoint
        transform.position += moveVector;

        // Check if the waypoint is reached
        if (directionToWaypoint.magnitude < reachThreshold)
        {
            // Increment waypoint index or reset to 0 if at the end
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            if (currentWaypointIndex == 0)
            {
                GameManager.instance.NextLevel();
            }
        }
    }

    void RotateTowardsWaypoint()
    {
        if (waypoints.Length == 0) return; // Exit if no waypoints are set

        // Direction from current position to the target waypoint
        Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;

        // Calculate the rotation needed to look at the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Rotate towards the waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void HandleCollision()
    {
        if (isMoving) // Prevent overlapping collision handling
        {
            StartCoroutine(CollisionResponseCoroutine());
            PlayerHealth.instance.ProcessHit();
            GameManager.instance.CrashAudio();
            GhostMover.instance.IncrementObstructionIndex();
        }
    }

    IEnumerator CollisionResponseCoroutine()
    {
        isMoving = false; // Stop movement immediately to simulate collision impact

        // Calculate the start and end positions for the backward movement
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - transform.forward * collisionMoveBackDistance;

        float elapsedTime = 0f;
        float moveBackTime = 0.5f; // Duration of the move back action. Adjust as needed.

        // Define a bouncy animation curve
        AnimationCurve bounceCurve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.2f, 1.2f), // Dynamic peak
            new Keyframe(0.5f, 0.6f), // Dynamic dip
            new Keyframe(0.8f, 1.1f), // Dynamic rise
            new Keyframe(1f, 1f)
        );

        // Smoothly move the player back with a bouncy effect over moveBackTime seconds
        while (elapsedTime < moveBackTime)
        {
            float t = bounceCurve.Evaluate(elapsedTime / moveBackTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the player is exactly at the end position after moving back
        transform.position = endPosition;

        yield return new WaitForSeconds(collisionPauseDuration); // Wait after moving back before resuming movement

        isMoving = true; // Resume normal movement
    }
}
