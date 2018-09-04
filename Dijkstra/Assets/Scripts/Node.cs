using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 nodePosition;
    public float nodeWeight;
    public bool nodeBlocked;
    public List<Node> connectedNodes = new List<Node>();
    public int numConnectedNodes = 0;

    public Node(Vector3 position, float weight, bool blocked)
    {
        nodePosition = position;
        nodeWeight = weight;
        nodeBlocked = blocked;
    }
}
