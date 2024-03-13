using System;
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

    [SerializeField] GameObject player;
    [SerializeField] GameObject ghostCube;
    [SerializeField] GameObject ghostCubePath;

    [SerializeField] List<Transform> obstructionPositions = new List<Transform>();
    private int nextObstructionIndex = 0;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (obstructionPositions.Count > 0)
        {
            SetGhostPosition(obstructionPositions[nextObstructionIndex].position);
        }
    }

    void Update()
    {
        PlayerInput();

        ghostCube.transform.localScale = transform.localScale;
        ghostCube.transform.rotation = player.transform.rotation;

        float distance = Vector3.Distance(transform.position, obstructionPositions[nextObstructionIndex].position);

        if (distance > 15)
        {
            ghostCube.SetActive(false);
            ghostCubePath.SetActive(false);
        }
        else
        {
            ghostCube.SetActive(true);
            ghostCubePath.SetActive(true);
        }

        SetGhostPath();
    }

    void PlayerInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        float newHeight = Mathf.Lerp(minHeight, maxHeight, Mathf.InverseLerp(-1f, 1f, verticalInput));
        float newWidth = Mathf.Lerp(minWidth, maxWidth, Mathf.InverseLerp(-1f, 1f, -verticalInput));

        // Update the scale of the original cube
        transform.localScale = new Vector3(newWidth, newHeight, transform.localScale.z);
    }

    void SetGhostPath()
    {
        float ghostCubePathLength = Vector3.Distance(transform.position, ghostCube.transform.position);

        Vector3 ghostCubePathSize = new Vector3(transform.localScale.x, transform.localScale.y, ghostCubePathLength);
        ghostCubePath.transform.localScale = ghostCubePathSize;
    }

    void SetGhostPosition(Vector3 position)
    {
        ghostCube.transform.position = new Vector3(position.x, transform.position.y, position.z);
    }

    public void HandleCollision()
    {
        GameManager.instance.PassAudio();
        // Increment the index to the next obstruction position
        nextObstructionIndex++;
        // Check if the next obstruction index is within the list bounds
        if (nextObstructionIndex < obstructionPositions.Count)
        {
            // Set the position of the ghost cube to the position of the next obstruction
            SetGhostPosition(obstructionPositions[nextObstructionIndex].position);
        }
        else
        {
            Debug.Log("No more obstructions.");
            // Optionally, you can disable the ghost cube or perform other actions when there are no more obstructions
        }
    }
}