using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public LayerMask floorOnly;
    public Transform target;
    public GameObject wall;
    public Text healthTextBox;
    public Text roundTextBox;
    public int numWallsCanPlace = 5;
    public int health = 200;
    public bool playerTurn = true;
    public bool canMoveTarget = true;
    public int roundNum = 1;

    FloorGrid grid;
    public Pathfinding pathfinding;
    EnemySpawn enemySpawn;
    GameObject wallPlaceholder;
    public int numFinished = 0;
    GameObject[] objectsToClear;

    // Use this for initialization
    void Start ()
    {
        grid = GetComponent<FloorGrid>();
        enemySpawn = GetComponent<EnemySpawn>();
        numWallsCanPlace = (roundNum * 2) + 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetWall();
        }
        else if (Input.GetMouseButtonDown(1) && canMoveTarget)
        {
            //MoveTarget();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (numWallsCanPlace == 0 && playerTurn == true)
        {
            playerTurn = false;
            foreach (GameObject gbj in enemySpawn.spawnLocations)
            {
                if (gbj.activeSelf)
                {
                    gbj.GetComponent<Pathfinding>().turn = true;
                }
            }
        }
        foreach (GameObject gbj in enemySpawn.spawnLocations)
        {
            if (gbj.GetComponent<Pathfinding>().hasRespawn == true)
            {
                numFinished++;
                gbj.GetComponent<Pathfinding>().hasRespawn = false;
            }
        }
        if (numFinished == enemySpawn.numEnemiesSpawn)
        {
            playerTurn = true;
            roundNum++;
            numWallsCanPlace = (roundNum * 2) + 3;
            numFinished = 0;
            objectsToClear = GameObject.FindGameObjectsWithTag("wall");
            foreach (GameObject gbj in objectsToClear)
            {
                Destroy(gbj);
            }
            grid.checkWall = true;
        }

        healthTextBox.text = "Health: " + health;
        roundTextBox.text = "Round: " + roundNum;
	}

    private void SetWall()
    {
        RaycastHit hitInfo;
        //Place an obstacle
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(intoScreen, out hitInfo, 1000000, floorOnly) && numWallsCanPlace > 0 && playerTurn)
        {
            Node newWall = grid.ConvertToGridPosition(new Vector3(hitInfo.point.x, 0, hitInfo.point.z));
            if (newWall.nodeBlocked == false)
            {
                wallPlaceholder = Instantiate(wall);
                wallPlaceholder.transform.position = newWall.nodePosition;
                grid.checkWall = true;
                numWallsCanPlace--;
            }
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
                //pathfinding.turn = true;
            }
        }
    }
}
