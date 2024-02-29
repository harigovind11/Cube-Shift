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
        if (collider.gameObject.CompareTag("obstr"))
        {
            playerMover.HandleCollision();

            collider.gameObject.SetActive(false);
        }
        else if (collider.CompareTag("Inside"))
        {
            ShapeShift.instance.HandleCollision();
        }
    }
}