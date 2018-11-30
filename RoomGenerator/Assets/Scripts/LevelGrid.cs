using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public GameObject basicRoom;

    public float nodeRadius;
    public Vector2 gridWorldSize;

    public int numRooms = 8;

    Node[,] grid;
    [HideInInspector]
    public List<Node> roomNodes = new List<Node>();

    [HideInInspector]
    public int gridSizeX, gridSizeY;
    float nodeDiameter;

    float roomScaleX, roomScaleZ;
    Vector3 roomScale;

    GameObject dummyRoom;
    int randomNumberX, randomNumberY;

	// Use this for initialization
	void Start ()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        //roomScaleX = 1 / (float)(gridSizeX * gridSizeX);
        //roomScaleZ = 1 / (float)(gridSizeY * gridSizeY);
        roomScaleX = 1 / (float)(5 * 5) * 2.5f;
        roomScaleZ = 1 / (float)(5 * 5) * 2.5f;
        roomScale = new Vector3(roomScaleX, 1, roomScaleZ);
        CreateGrid();
        SetNeighborNodes();
        SetRooms();
        SpawnRooms();
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

    public void SetRooms()
    {
        if (grid != null)
        {
            randomNumberX = Random.Range(0, gridSizeX);
            randomNumberY = Random.Range(0, gridSizeY);

            Node currentNode = grid[randomNumberX, randomNumberY];
            currentNode.isRoom = true;
            roomNodes.Add(currentNode);

            for (int i = 0; i < numRooms - 1; i++)
            {
                Dictionary<int, Node> possibleRooms = new Dictionary<int, Node>();
                foreach (Node n in currentNode.neighbors.Values)
                {
                    if (!n.isRoom)
                    {
                        possibleRooms.Add(possibleRooms.Count, n);
                    }
                }
                while (currentNode.isRoom)
                {
                    if (possibleRooms.Count > 0)
                    {
                        int randomNum = Random.Range(0, possibleRooms.Count);
                        Node tempNode = possibleRooms[randomNum];
                        if (!tempNode.isRoom)
                        {
                            currentNode = tempNode;
                        }
                    }
                    else if (possibleRooms.Count == 0)
                    {
                        int randomRoom = Random.Range(0, roomNodes.Count);
                        currentNode = roomNodes[randomRoom];
                        foreach (Node n in currentNode.neighbors.Values)
                        {
                            if (!n.isRoom)
                            {
                                possibleRooms.Add(possibleRooms.Count, n);
                            }
                        }
                    }
                }
                currentNode.isRoom = true;
                roomNodes.Add(currentNode);
            }
        }
    }

    public void SpawnRooms()
    {
        foreach (Node n in roomNodes)
        {
            dummyRoom = Instantiate(basicRoom);
            dummyRoom.GetComponent<GenerateWalls>().sourceNode = n;
            dummyRoom.transform.position = n.position;
            dummyRoom.transform.localScale = roomScale;
            n.attachedRoom = dummyRoom;
        }
    }

    public void SetNeighborNodes()
    {
        foreach(Node n in grid)
        {
            n.neighbors = GetNeighborNodes(n);
        }
    }

    public Dictionary<string, Node> GetNeighborNodes(Node node)
    {
        Dictionary<string, Node> neighborNodes = new Dictionary<string, Node>();
        int xCheck;
        int yCheck;

        //Right Side
        xCheck = node.gridX + 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add("right", grid[xCheck, yCheck]);
            }
        }

        //Left Side
        xCheck = node.gridX - 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add("left", grid[xCheck, yCheck]);
            }
        }

        //Top Side
        xCheck = node.gridX;
        yCheck = node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add("top", grid[xCheck, yCheck]);
            }
        }

        //Bottom Side
        xCheck = node.gridX;
        yCheck = node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add("bottom", grid[xCheck, yCheck]);
            }
        }

        return neighborNodes;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.2f, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (!node.isRoom)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(node.position, new Vector3(1 * (nodeDiameter - 0.1f), 0.2f, 1 * (nodeDiameter - 0.1f)));
                }
                else if (node.isRoom)
                {
                    Gizmos.color = Color.yellow;
                }
            }
        }
    }
}
