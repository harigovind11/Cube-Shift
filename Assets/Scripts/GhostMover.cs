using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMover : MonoBehaviour

{
    public static GhostMover instance;


    [SerializeField] GameObject player;
    [SerializeField] GameObject ghostCube;
    [SerializeField] GameObject ghostCubePath;

    [SerializeField] List<Transform> obstructionPositions = new List<Transform>();
    [SerializeField] int nextObstructionIndex = 0;

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
        // Ensure that nextObstructionIndex is within the bounds of obstructionPositions
        if (nextObstructionIndex < 0 || nextObstructionIndex >= obstructionPositions.Count)
        {
            nextObstructionIndex = 0;
        }

        ghostCube.transform.localScale = transform.localScale;
        ghostCube.transform.rotation = player.transform.rotation;

        // Safely access the element in the list now that we've checked the index
        Vector3 targetPosition = obstructionPositions[nextObstructionIndex].position;
        float distance = Vector3.Distance(transform.position, targetPosition);


        ghostCube.SetActive(distance < 15);
        ghostCubePath.SetActive(distance < 15);

        if (ghostCube.activeSelf)
        {
            SetGhostPath();
        }
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
        nextObstructionIndex++;
        if (nextObstructionIndex < obstructionPositions.Count)
        {
            SetGhostPosition(obstructionPositions[nextObstructionIndex].position);
        }
        else
        {
            Debug.Log("No more obstructions.");
        }
    }

    public void IncrementObstructionIndex()
    {
        nextObstructionIndex++;
    }
}