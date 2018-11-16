using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public float nodeRadius;
    public Vector2 gridWorldSize;

    Node[,] grid;

    int gridSizeX, gridSizeY;
    float nodeDiameter;

	// Use this for initialization
	void Start ()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        foreach (Node node in grid)
        {
            print(node.position);
        }
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                grid[x, y] = new Node(false, worldPoint, x, y);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.2f, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(node.position, new Vector3(1 * (nodeDiameter - 0.1f), 0.2f, 1 * (nodeDiameter - 0.1f)));
            }
        }
    }
}
