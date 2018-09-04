using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 nodePosition;
    public float nodeWeight;

    public Node(Vector3 position, float weight)
    {
        nodePosition = position;
        nodeWeight = weight;
    }
}
