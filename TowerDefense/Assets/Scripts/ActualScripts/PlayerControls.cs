using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public LayerMask floorOnly;
    public Transform target;

    FloorGrid grid;
    Pathfinding pathfinding;

    // Use this for initialization
    void Start ()
    {
        grid = GetComponent<FloorGrid>();
        pathfinding = GetComponent<Pathfinding>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveTarget();
	}

    private void SetWall()
    {
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    private void MoveTarget()
    {
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(1))
        {
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(intoScreen, out hitInfo, 1000000, floorOnly))
            {
                target.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                Node newTarget = grid.ConvertToGridPosition(target.position);
                target.position = newTarget.nodePosition;
                pathfinding.turn = true;
            }
        }
    }
}
