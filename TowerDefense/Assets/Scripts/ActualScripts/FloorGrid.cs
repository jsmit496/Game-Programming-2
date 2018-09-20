using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Issue: Nodes are similar can't make different right now

public class FloorGrid : MonoBehaviour
{
    public LayerMask wall;
    public Transform character;
    public Transform target;
    public GameObject floorTile;
    public float gridWidth = 15;
    public float gridHeight = 10;
    public float nodeTileSize = 1;
    public bool checkWall = false;

    private int numTilesWidth;
    private int numTilesHeight;

    Node[,] grid;

	// Use this for initialization
	void Start ()
    {
        numTilesWidth = Mathf.RoundToInt(gridWidth / nodeTileSize);
        numTilesHeight = Mathf.RoundToInt(gridHeight / nodeTileSize);
        CreateGrid();
        BuildLevel();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (checkWall)
        {
            checkForNewWall();
        }
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
                bool walledPath = (Physics.CheckSphere(gridPoint, nodeTileSize / 2, wall));
                print(walledPath);
                grid[x, y] = new Node(gridPoint, walledPath, x, y);
            }
        }

        //Find all connected nodes to nodes
        for (int x = 0; x < numTilesWidth; x++)
        {
            for (int y = 0; y < numTilesHeight; y++)
            {
                if (x + 1 <= numTilesWidth - 1)
                {
                    grid[x, y].connections.Add(grid[x + 1, y]);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (x - 1 >= 0)
                {
                    grid[x, y].connections.Add(grid[x - 1, y]);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (y + 1 <= numTilesHeight - 1)
                {
                    grid[x, y].connections.Add(grid[x, y + 1]);
                    grid[x, y].numConnectedNodes += 1;
                }
                if (y - 1 >= 0)
                {
                    grid[x, y].connections.Add(grid[x, y - 1]);
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

    public void checkForNewWall()
    {
        for (int x = 0; x < numTilesWidth; x++)
        {
            for (int y = 0; y < numTilesHeight; y++)
            {
                bool walledPath = (Physics.CheckSphere(grid[x,y].nodePosition, nodeTileSize / 2, wall));
                if (walledPath)
                {
                    grid[x, y].nodeBlocked = true;
                }
                else
                {
                    grid[x, y].nodeBlocked = false;
                }
            }
        }
    }

    public void BuildLevel()
    {
        foreach(Node n in grid)
        {
            //Instantiate a black floor tile at the position of each node
            Instantiate(floorTile, n.nodePosition, floorTile.transform.rotation);
        }
    }

    public List<Node> path;
    public List<Node> checkClosed;
    private void OnDrawGizmos()
    {
        //Display the Grid size
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, 1, gridHeight));

        //Display the grid's tiles/nodes
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                if (n.nodeBlocked == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawCube(n.nodePosition, new Vector3(nodeTileSize - 0.1f,0.1f,nodeTileSize-0.1f));
            }
            if (path != null)
            {
                foreach (Node n in path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.nodePosition, new Vector3(nodeTileSize - 0.1f, 0.1f, nodeTileSize - 0.1f));
                }
            }
        }
    }
}
