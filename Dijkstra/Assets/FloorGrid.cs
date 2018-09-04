using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Issue: Nodes are similar can't make different right now

public class FloorGrid : MonoBehaviour
{
    public float gridWidth = 20;
    public float gridHeight = 20;
    public float nodeTileSize = 1;

    private int numTilesWidth;
    private int numTilesHeight;

    Node[,] grid;

	// Use this for initialization
	void Start ()
    {
        numTilesWidth = Mathf.RoundToInt(gridWidth / nodeTileSize);
        numTilesHeight = Mathf.RoundToInt(gridHeight / nodeTileSize);
        CreateGrid();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    public void CreateGrid()
    {
        grid = new Node[numTilesWidth, numTilesHeight];
        Vector3 gridStart = new Vector3(transform.position.x - gridWidth / 2 + nodeTileSize / 2, transform.position.y, transform.position.z - gridHeight / 2 + nodeTileSize / 2);

        //Test gridStart Position
        //print(gridStart);

        for (int x = 0; x < numTilesWidth; x++)
        {
            for (int y = 0; y < numTilesHeight; y++)
            {
                Vector3 gridPoint = new Vector3(gridStart.x + (nodeTileSize * x), gridStart.y, gridStart.z + (nodeTileSize * y));
                //print(gridPoint);
                grid[x, y] = new Node(gridPoint, 1);
            }
        }
    }
    
    public Node StartNodePosition(Vector3 position)
    {
        float percentX = (position.x + gridWidth / 2) / gridWidth / 2;
        float percentY = (position.z + gridHeight / 2) / gridHeight / 2;
        int x = Mathf.RoundToInt((numTilesWidth - 1) * percentX);
        int y = Mathf.RoundToInt((numTilesHeight - 1) * percentY);
        return grid[x, y];
    }
    

    private void OnDrawGizmos()
    {
        //Display the Grid size
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, 1, gridHeight));

        //Display the grid's tiles/nodes
        if (grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(n.nodePosition, new Vector3(1, 0.1f, 1) * nodeTileSize);
            }
        }
    }
}
