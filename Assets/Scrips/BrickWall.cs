using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField] private GameObject[] brickPrefabs;
    [SerializeField] private int nRows = 5;
    [SerializeField] private int nCols = 5;
    [SerializeField] private float gap = 0.5f;

    private void Start()
    {
        PlaceBricks(nRows, nCols, gap);
    }

    private void PlaceBricks(int nRows = 5, int nCols = 5, float gap = 0.5f)
    {
        if (brickPrefabs == null || brickPrefabs.Length == 0)
        {
            Debug.LogError("No brick prefabs assigned!");
            return;
        }

        float brickWidth = brickPrefabs[0].GetComponent<Renderer>().bounds.size.x;
        float brickHeight = brickPrefabs[0].GetComponent<Renderer>().bounds.size.y;
        
        float totalWidth = nCols * (brickWidth + gap) - gap;
        float totalHeight = nRows * (brickHeight + gap) - gap;
        
        Vector3 startPosition = transform.position - new Vector3((totalWidth / 2)-0.5f, (totalHeight / 2)-4, 0);

        for (int row = 0; row < nRows; row++)
        {
            GameObject brickPrefab = brickPrefabs[row % brickPrefabs.Length];

            for (int col = 0; col < nCols; col++)
            {
                Vector3 brickPosition = startPosition + new Vector3(col * (brickWidth + gap), -row * (brickHeight + gap), 0);
                GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                brick.AddComponent<Brick>(); 
            }
        }
    }
}