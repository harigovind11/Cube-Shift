using UnityEngine;


public class CubeSplitter : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numSlices = 4;

    void Start()
    {
        SplitCube();
    }

    void SplitCube()
    {
        // Calculate the size of each slice
        float sliceSize = transform.localScale.x / numSlices;

        // Create slices
        for (int i = 0; i < numSlices; i++)
        {
            // Calculate position for the slice
            Vector3 slicePosition = transform.position + new Vector3(sliceSize * i, 0f, 0f);

            // Instantiate a slice cube
            GameObject sliceCube = Instantiate(cubePrefab, slicePosition, Quaternion.identity, transform);

            // Set size of the slice cube
            sliceCube.transform.localScale = new Vector3(sliceSize, transform.localScale.y, transform.localScale.z);
        }
    }
}