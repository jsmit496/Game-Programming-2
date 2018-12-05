using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGrid : MonoBehaviour
{
    public float nodeRadius;
    public LayerMask unwalkable;
    [HideInInspector]
    public Vector2 gridRoomSize;

    RoomNode[,] grid;
    [HideInInspector]
    public List<RoomNode> tileNodes = new List<RoomNode>();

    int gridSizeX, gridSizeY;
    float nodeDiameter;

    // Use this for initialization
    void Start ()
    {
        gridRoomSize.x = transform.localScale.x * 10;
        gridRoomSize.y = transform.localScale.z * 10;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridRoomSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridRoomSize.y / nodeDiameter);
        CreateGrid();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreateGrid()
    {
        grid = new RoomNode[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridRoomSize.x / 2 - Vector3.forward * gridRoomSize.y / 2;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool blocked = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, unwalkable))
                {
                    blocked = false;
                }
                grid[x, y] = new RoomNode(blocked, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridRoomSize.x, 0.2f, gridRoomSize.y));

        if (grid != null)
        {
            foreach (RoomNode node in grid)
            {
                if (!node.isBlocked)
                {
                    Gizmos.color = Color.red;
                }
                else if (node.isBlocked)
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawCube(node.position, new Vector3(1 * (nodeDiameter - 0.1f), 0.05f, 1 * (nodeDiameter - 0.1f)));
            }
        }
    }
}
