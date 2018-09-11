using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform player;
    public Transform target;

    FloorGrid grid;

	// Use this for initialization
	void Start ()
    {
        grid = GetComponent<FloorGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FindPath(player.position, target.position);
	}

    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.ConvertToGridPosition(startPosition);
        Node targetNode = grid.ConvertToGridPosition(targetPosition);

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        openList.Add(startNode);

        while(openList.Count > 0 && !closedList.Contains(targetNode))
        {
            Node currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].weight <= currentNode.weight)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node connectedNodes in currentNode.connections)
            {
                if (!connectedNodes.nodeBlocked || closedList.Contains(connectedNodes))
                {
                    //Do Nothing
                    continue;
                }

                int newWeightForConnection = currentNode.weight + GetDistance(currentNode, connectedNodes);

                if (newWeightForConnection < connectedNodes.weight && !openList.Contains(connectedNodes))
                {
                    connectedNodes.weight = newWeightForConnection;
                    connectedNodes.parent = currentNode;

                    if (!openList.Contains(connectedNodes))
                    {
                        openList.Add(connectedNodes);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        //Test Gizmos
        grid.path = path;
    }

    int GetDistance(Node nodeA, Node NodeB)
    {
        int distanceX = Mathf.Abs(nodeA.PosX - NodeB.PosY);
        int distanceY = Mathf.Abs(nodeA.PosY - NodeB.PosX);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
