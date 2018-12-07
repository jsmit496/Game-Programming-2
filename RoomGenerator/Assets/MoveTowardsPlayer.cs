using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform startPosition;
    public Transform targetPosition;

    public float speed = 4.0f;

    RoomNode targetNode;

    Vector3 goToPosition;

    public List<RoomNode> allPathNodes = new List<RoomNode>();
    List<RoomNode> path = new List<RoomNode>();

    RoomGrid roomGrid;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        FindPath(startPosition.position, targetPosition.position);

        if (path != null && startPosition.position != targetPosition.position && path.Count > 0)
        {
            targetNode = path[0];
            goToPosition = new Vector3(targetNode.position.x, startPosition.position.y, targetNode.position.z);
        }
        else if (startPosition.position == targetPosition.position)
        {
            path = null;
            targetNode = null;
        }

        startPosition.position = Vector3.MoveTowards(startPosition.position, goToPosition, speed * Time.deltaTime);
        Vector3 targetDir = goToPosition - startPosition.position;
        Vector3 newDir = Vector3.RotateTowards(startPosition.forward, targetDir, speed * Time.deltaTime, 0.0f);
        startPosition.rotation = Quaternion.LookRotation(newDir);
    }

    public RoomNode NodeFromWorldPosition(Vector3 _worldPosition)
    {
        int x = (int)_worldPosition.x;
        int y = (int)_worldPosition.y;
        int z = (int)_worldPosition.z;
        _worldPosition = new Vector3(x, y, z);
        foreach (RoomNode rn in allPathNodes)
        {
            if (rn.position == _worldPosition)
            {
                return rn;
            }
        }
        return null;
    }

    void FindPath(Vector3 _startPosition, Vector3 _targetPosition)
    {
        RoomNode startNode = NodeFromWorldPosition(_startPosition);
        RoomNode targetNode = NodeFromWorldPosition(_targetPosition);

        List<RoomNode> openList = new List<RoomNode>();
        HashSet<RoomNode> closedList = new HashSet<RoomNode>();

        openList.Add(startNode);

        while (openList.Count > 0 && !closedList.Contains(targetNode))
        {
            RoomNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetPath(startNode, targetNode);
            }

            foreach (RoomNode rn in allPathNodes)
            {
                foreach (RoomNode neighborNode in rn.neighbors)
                {
                    if (neighborNode.isBlocked || closedList.Contains(neighborNode))
                    {
                        continue;
                    }
                    int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);

                    if (moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                    {
                        neighborNode.gCost = moveCost;
                        neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                        neighborNode.parent = currentNode;

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
            }
        }
    }

    void GetPath(RoomNode _startingNode, RoomNode _endNode)
    {
        List<RoomNode> pathCreate = new List<RoomNode>();
        RoomNode currentNode = _endNode;

        while (currentNode != _startingNode)
        {
            pathCreate.Add(currentNode);
            currentNode = currentNode.parent;
        }

        pathCreate.Reverse();

        path = pathCreate;
    }

    int GetManhattenDistance(RoomNode _nodeA, RoomNode _nodeB)
    {
        int x = Mathf.Abs(_nodeA.gridX - _nodeB.gridX);
        int y = Mathf.Abs(_nodeA.gridY - _nodeB.gridY);

        return x + y;
    }

    void FollowPath(List<RoomNode> pathToFollow)
    {
        float moveSpeed = speed * Time.deltaTime;
        foreach (RoomNode node in pathToFollow)
        {
            while (startPosition.position != node.position)
            {
                startPosition.position = Vector3.Lerp(startPosition.position, node.position, moveSpeed);
            }
        }
    }
}
