using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform target;
    public int numEnemiesSpawn = 3;
    public GameObject[] spawnLocations;
    public bool respawn = false;


    FloorGrid floorGrid;
    PlayerControls playerControls;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject enemyPlaceholder;

	// Use this for initialization
	void Start ()
    {
        floorGrid = GetComponent<FloorGrid>();
        playerControls = GetComponent<PlayerControls>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Spawn();
        Respawn();
	}

    public void Spawn()
    {
        numEnemiesSpawn = playerControls.roundNum + 2;

        if (numEnemiesSpawn > 15)
        {
            int extraEnergy = (numEnemiesSpawn - 15) * 2;
            foreach (GameObject gbj in spawnLocations)
            {
                gbj.GetComponent<Pathfinding>().maxEnergy = 20 + extraEnergy;
            }
            numEnemiesSpawn = 15;
        }

        for (int i = 0; i < numEnemiesSpawn; i++)
        {
            spawnLocations[i].SetActive(true);
        }
    }

    public void Respawn()
    {
        foreach (GameObject gbj in spawnLocations)
        {
            if (gbj.transform.position == target.transform.position || gbj.GetComponent<Pathfinding>().energy <= 0)
            {
                if (gbj.GetComponent<Pathfinding>().energy <= 0)
                {
                    gbj.GetComponent<Pathfinding>().energy = 1;
                }
                gbj.transform.position = gbj.GetComponent<Pathfinding>().startPosition;
                gbj.GetComponent<Pathfinding>().hasRespawn = true;
                gbj.GetComponent<Pathfinding>().pathToFollow = null;
            }
        }
    }
}
