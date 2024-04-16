using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShift : MonoBehaviour
{
    public static ShapeShift instance;

    [SerializeField] float minWidth = 1f;
    [SerializeField] float maxWidth = 5f;
    [SerializeField] float minHeight = 1f;
    [SerializeField] float maxHeight = 5f;

    [SerializeField] float scaleSpeed = 0.005f; // Increase or decrease to adjust sensitivity


    private Vector2 startPosition;
    private bool isInteracting = false;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.touchSupported && Input.touchCount > 0)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();
        }
    }

    void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPosition = touch.position;
                isInteracting = true;
                break;
            case TouchPhase.Moved:
                if (isInteracting)
                {
                    AdjustScale(touch.position);
                }

                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isInteracting = false;
                break;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            isInteracting = true;
        }
        else if (Input.GetMouseButton(0) && isInteracting)
        {
            AdjustScale(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isInteracting = false;
        }
    }

    void AdjustScale(Vector2 currentPosition)
    {
        float verticalMovement = currentPosition.y - startPosition.y;
        float normalizedMovement = Mathf.Clamp(verticalMovement * scaleSpeed, -1f, 1f);

        float newHeight = Mathf.Lerp(minHeight, maxHeight, (normalizedMovement + 1) / 2);
        float newWidth = Mathf.Lerp(minWidth, maxWidth, 1 - (normalizedMovement + 1) / 2);

        transform.localScale = new Vector3(newWidth, newHeight, transform.localScale.z);
    }
}