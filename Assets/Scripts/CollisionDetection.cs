using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerMover playerMover;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("obstr"))
        {
            playerMover.HandleCollision();
            

            collider.gameObject.SetActive(false);
            // collider.gameObject.GetComponentInParent<BoxCollider>().enabled = false;
        }
        else if (collider.CompareTag("Inside"))
        {
            ShapeShift.instance.HandleCollision();
        }
    }
}