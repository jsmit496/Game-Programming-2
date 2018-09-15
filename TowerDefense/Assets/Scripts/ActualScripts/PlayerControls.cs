using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public LayerMask floorOnly;
    public Transform target;
    public GameObject wall;
    public int numWallsCanPlace = 5;
    public int health = 200;
    public bool playerTurn = true;

    FloorGrid grid;
    public Pathfinding pathfinding;
    GameObject wallPlaceholder;

    // Use this for initialization
    void Start ()
    {
        grid = GetComponent<FloorGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetWall();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            MoveTarget();
        }
	}

    private void SetWall()
    {
        RaycastHit hitInfo;
        //Place an obstacle
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(intoScreen, out hitInfo, 1000000, floorOnly) && numWallsCanPlace > 0 && playerTurn)
        {
            wallPlaceholder = GameObject.Instantiate(wall);
            Node newWall = grid.ConvertToGridPosition(new Vector3(hitInfo.point.x, 0, hitInfo.point.z));
            wallPlaceholder.transform.position = newWall.nodePosition;
            grid.checkWall = true;
            numWallsCanPlace--;
        }
    }

    private void MoveTarget()
    {
        RaycastHit hitInfo;
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(intoScreen, out hitInfo, 1000000, floorOnly) )
        {
            Node newTarget = grid.ConvertToGridPosition(new Vector3(hitInfo.point.x, 0, hitInfo.point.z));
            if (newTarget.nodeBlocked == false)
            {
                target.position = newTarget.nodePosition;
                //Test for whenever i place a new target for the AI to move toward
                pathfinding.turn = true;
            }
        }
    }
}
