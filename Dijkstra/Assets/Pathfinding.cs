using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    FloorGrid grid;

	// Use this for initialization
	void Start ()
    {
        grid = GetComponent<FloorGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        //Note: need to change name for "StartNodePosition"
        Node startNode = grid.StartNodePosition(startPosition);
        Node targetNode = grid.StartNodePosition(targetPosition);

        List<Node> openNodes = new List<Node>();
        List<Node> closedNodes = new List<Node>();
        openNodes.Add(startNode);
    }
}
