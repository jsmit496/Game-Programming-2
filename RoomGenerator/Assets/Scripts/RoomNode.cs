using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public bool isBlocked;
    public int gridX, gridY;

    public Vector3 position;

    public Dictionary<string, Node> neighbors;

    public RoomNode(bool _isBlocked, Vector3 _position, int _gridX, int _gridY)
    {
        isBlocked = _isBlocked;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
