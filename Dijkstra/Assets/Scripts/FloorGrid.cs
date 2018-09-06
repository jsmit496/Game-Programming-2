using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Issue: Nodes are similar can't make different right now

public class FloorGrid : MonoBehaviour
{
    public Transform character;
    public Transform target;
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

        for (int x = 0; x < numTilesWidth; x++)
        {
            for (int y = 0; y < numTilesHeight; y++)
            {
                Vector3 gridPoint = new Vector3(gridStart.x + (nodeTileSize * x), gridStart.y, gridStart.z + (nodeTileSize * y));
                grid[x, y] = new Node(gridPoint, false);
            }
        }

        //Find all connected nodes to nodes
        for (int x = 0; x < numTilesWidth; x++)
        {
            for (int y = 0; y < numTilesHeight; y++)
            {
                if (x + 1 <= numTilesWidth - 1 && grid[x + 1, y].nodeBlocked == false)
                {
                    grid[x, y].connections.Add(grid[x + 1, y], 1);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (x - 1 >= 0 && grid[x - 1, y].nodeBlocked == false)
                {
                    grid[x, y].connections.Add(grid[x - 1, y], 1);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (y + 1 <= numTilesHeight - 1 && grid[x, y + 1].nodeBlocked == false)
                {
                    grid[x, y].connections.Add(grid[x, y + 1], 1);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (y - 1 >= 0 && grid[x, y - 1].nodeBlocked == false)
                {
                    grid[x, y].connections.Add(grid[x, y - 1], 1);
                    grid[x, y].numConnectedNodes += 1;
                }
                print("grid[" + x + ", " + y + "] is " + grid[x, y].numConnectedNodes);
            }
        }
    }
    
    public Node ConvertToGridPosition(Vector3 position)
    {
        float percentX = (position.x + gridWidth / 2) / gridWidth;
        float percentY = (position.z + gridHeight / 2) / gridHeight;
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
            Node player = ConvertToGridPosition(character.position);
            Node goal = ConvertToGridPosition(target.position);
            foreach (Node n in grid)
            {
                Gizmos.color = Color.white;
                if (player == n)
                {
                    Gizmos.color = Color.blue;
                }
                if (goal == n)
                {
                    Gizmos.color = Color.magenta;
                }
                //Gizmos.DrawCube(n.nodePosition, new Vector3(1, 0.1f, 1) * nodeTileSize);
                Gizmos.DrawSphere(n.nodePosition, 0.3f * nodeTileSize);
            }
        }
    }
}
