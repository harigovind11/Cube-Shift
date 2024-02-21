using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShift : MonoBehaviour
{

    [SerializeField] float minWidth = 1f;
    [SerializeField] float maxWidth = 5f;
    [SerializeField] float minHeight = 1f;
    [SerializeField] float maxHeight = 5f;
    
    void Update()
    {

        float verticalInput = Input.GetAxis("Vertical");
        
        float newHeight = Mathf.Lerp(minHeight, maxHeight, Mathf.InverseLerp(-1f, 1f, verticalInput));
        float newWidth = Mathf.Lerp(minWidth, maxWidth, Mathf.InverseLerp(-1f, 1f, -verticalInput));
        
        transform.localScale = new Vector3(newWidth, newHeight, transform.localScale.z);
        

    }
}
