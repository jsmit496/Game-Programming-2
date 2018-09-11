using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float speed = 0.1f;

    FloorGrid grid;
    List<Node> pathToFollow = new List<Node>();

	// Use this for initialization
	void Start ()
    {
        grid = GetComponent<FloorGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindPath(player.position, target.position);
        }
        for (int i = 0; i < pathToFollow.Count; i++)
        {
            //Move Character(player)
            while(player.position != pathToFollow[i].nodePosition)
            {
                player.position = Vector3.MoveTowards(player.position, pathToFollow[i].nodePosition, speed * Time.deltaTime);
            }
        }
	}

    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.ConvertToGridPosition(startPosition);
        Node targetNode = grid.ConvertToGridPosition(targetPosition);

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        openList.Add(startNode);

        while(openList.Count > 0)
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
            grid.checkClosed = closedList;

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node connectedNodes in currentNode.connections)
            {
                if (connectedNodes.nodeBlocked || closedList.Contains(connectedNodes))
                {
                    //Do Nothing
                    continue;
                }
                print("connectedNodes = " + connectedNodes.nodePosition);
                print("currentNode = " + currentNode.nodePosition);
                print(GetDistance(currentNode, targetNode));
                print(GetDistance(connectedNodes, targetNode));
                int oldWeightForConnection = GetDistance(currentNode, targetNode);
                int newWeightForConnection = GetDistance(connectedNodes, targetNode);

                if (newWeightForConnection > oldWeightForConnection && !openList.Contains(connectedNodes))
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
            print("targetNode = " + targetNode.nodePosition);
            print("currentNode.parent = " + currentNode.parent.nodePosition);
            print("currentNode = " + currentNode.nodePosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        //Test Gizmos
        grid.path = path;
        pathToFollow = path;
        for (int i = 0; i < pathToFollow.Count; i++)
        {
            print("node: " + i + " = " + pathToFollow[i].nodePosition);
        }
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
