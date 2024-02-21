using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    float impactForce = 5f;
    float impactEffectDuration = 0.5f;
    bool isColliding = false;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("obstr") && !isColliding)
        {
            StartCrashSequence(collision.gameObject);
        }
    }


    void StartCrashSequence(GameObject obstruction)
    {
        isColliding = true;

        //TODO: Particle Effect

        GetComponent<PlayerMover>().enabled = false;
        //
        obstruction.GetComponent<BoxCollider>().enabled = false;
        //
        // ImpactEffect();
        StartCoroutine(ImpactEffect1());
        // StartCoroutine(EnablePlayerMoverAfterDelay());
    }

    void ImpactEffect()
    {
        Vector3 direction = transform.position;
        direction = -direction.normalized;

        GetComponent<Rigidbody>().velocity = direction * impactForce;
    }

    IEnumerator ImpactEffect1()
    {
        // Remember the original position
        Vector3 originalPosition = transform.position;

        // Apply the impact force in the z-axis
        float newZPosition = transform.position.z + impactForce * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        // Wait for a short duration (you can adjust this value)
        yield return new WaitForSeconds(0.5f);

        // Move the object back to its original position
        while (transform.position.z != originalPosition.z)
        {
            float step = 1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);
            yield return null;
        }

        // Ensure the object is exactly at the original position
        transform.position = originalPosition;

        StartCoroutine(EnablePlayerMoverAfterDelay());
    }

    IEnumerator EnablePlayerMoverAfterDelay()
    {
        yield return new WaitForSeconds(impactEffectDuration);

        GetComponent<PlayerMover>().enabled = true;
        isColliding = false;
    }
}