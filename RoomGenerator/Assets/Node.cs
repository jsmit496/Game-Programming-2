using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isRoom;
    public int gridX, gridY;

    public Vector2 position;

    public Node(bool _isRoom, Vector3 _position, int _gridX, int _gridY)
    {
        isRoom = _isRoom;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
