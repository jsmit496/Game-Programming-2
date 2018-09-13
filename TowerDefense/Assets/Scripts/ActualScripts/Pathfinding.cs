using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float speed = 4.0f;
    public bool turn = true;

    private bool onlyOnce = true;
    private int count = 0;

    FloorGrid grid;
    List<Node> pathToFollow = new List<Node>();

    // Use this for initialization
    void Start ()
    {
        grid = GetComponent<FloorGrid>();
        count = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (turn == true)
        {
            onlyOnce = true;
            FindPath(player.position, target.position);
            turn = false;
            count = 0;
        }
        if (pathToFollow != null && count < pathToFollow.Count)
        {
            player.position = Vector3.MoveTowards(player.position, new Vector3(pathToFollow[count].nodePosition.x, 0, pathToFollow[count].nodePosition.z), speed * Time.deltaTime);
        }
        if (pathToFollow != null && count < pathToFollow.Count && player.position == new Vector3(pathToFollow[count].nodePosition.x, 0, pathToFollow[count].nodePosition.z))
        {
            count++;
        }
    }

    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.ConvertToGridPosition(startPosition);
        Node targetNode = grid.ConvertToGridPosition(targetPosition);

        if (onlyOnce)
        {
            pathToFollow = new List<Node>();
            onlyOnce = false;
        }

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        print("The start of the open list count: " + openList.Count);
        print("The start of the closed list count: " + closedList.Count);
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
                float oldDistance = Vector3.Distance(currentNode.nodePosition, targetNode.nodePosition);
                float newDistance = Vector3.Distance(connectedNodes.nodePosition, targetNode.nodePosition);
                //int oldWeightForConnection = GetDistance(currentNode, targetNode);
                //int newWeightForConnection = GetDistance(connectedNodes, targetNode);

                if (newDistance < oldDistance && !openList.Contains(connectedNodes))
                {
                    connectedNodes.weight = currentNode.weight + connectedNodes.weight;
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
        pathToFollow = path;
        for (int i = 0; i < pathToFollow.Count; i++)
        {
            print("node: " + i + " = " + pathToFollow[i].nodePosition);
        }
    }
}
