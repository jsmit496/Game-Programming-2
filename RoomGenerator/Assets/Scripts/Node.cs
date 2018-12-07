using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isRoom;
    public bool isStart;
    public bool isEnd;
    public bool containsChest;
    public bool containsEnemy;
    public int gridX, gridY;

    public Vector3 position;

    public Dictionary<string, Node> neighbors;

    public GameObject attachedRoom;

    public Node(bool _isRoom, Vector3 _position, int _gridX, int _gridY)
    {
        isRoom = _isRoom;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
