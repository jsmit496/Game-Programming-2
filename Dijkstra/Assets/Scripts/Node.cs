using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 nodePosition;
    //public float weight;
    public bool nodeBlocked;
    //public List<Node> connectedNodes = new List<Node>();
    public Dictionary<Node, float> connections = new Dictionary<Node, float>();
    public int numConnectedNodes = 0;

    public Node(Vector3 position, bool blocked)
    {
        nodePosition = position;
        //nodeWeight = weight;
        nodeBlocked = blocked;
    }
}
