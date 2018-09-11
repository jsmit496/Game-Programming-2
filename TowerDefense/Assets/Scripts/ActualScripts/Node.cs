using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 nodePosition;
    public int weight = 1;
    public bool nodeBlocked;
    public List<Node> connections = new List<Node>();
    public int PosX;
    public int PosY;
    //public Dictionary<Node, float> connections = new Dictionary<Node, float>();
    public int numConnectedNodes = 0;
    public Node parent;

    public Node(Vector3 position, bool blocked, int nodeXPos, int nodeYPos)
    {
        nodePosition = position;
        //nodeWeight = weight;
        nodeBlocked = blocked;
        PosX = nodeXPos;
        PosY = nodeYPos;
    }
}
