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
        Node startNode = grid.ConvertToGridPosition(startPosition);
        Node targetNode = grid.ConvertToGridPosition(targetPosition);

        List<Node> wayPoints = new List<Node>();
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        openList.Add(startNode);

        while(openList.Count > 0 && !closedList.Contains(targetNode))
        {
            openList.Sort();

        }
        

    }
}
