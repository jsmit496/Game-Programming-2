using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public bool isBlocked;
    public bool containsChest;
    public bool containsEnemy;
    public int gridX, gridY;

    public Vector3 position;

    public RoomNode parent;

    public int gCost;
    public int hCost;

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public List<RoomNode> neighbors;

    public RoomNode(bool _isBlocked, Vector3 _position, int _gridX, int _gridY)
    {
        isBlocked = _isBlocked;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
}
