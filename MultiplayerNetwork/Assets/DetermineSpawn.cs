using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineSpawn : MonoBehaviour
{
    public string weapon;
    public float waitSpawn = 10f;

    [SerializeField]
    private float waitSpawnCount = 0;

    private bool canSpawn = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleSpawnWeapon();
	}

    //NOTE: This will change the spawn every 10 seconds until it is changed to spawn when item is missing
    private void HandleSpawnWeapon()
    {
        if (canSpawn)
        {
            int randomNumber = Random.Range(0, 5);

            if (randomNumber == 0)
            {
                weapon = "Single Fire";
            }
            else if (randomNumber == 1)
            {
                weapon = "Rapid Fire";
            }
            else if (randomNumber == 2)
            {
                weapon = "Shotgun";
            }
            canSpawn = false;
            waitSpawnCount = waitSpawn;
            //Add another "else if" for explosion if it works
        }
        else if (!canSpawn)
        {
            waitSpawnCount -= Time.deltaTime;
            if (waitSpawnCount <= 0)
            {
                waitSpawnCount = 0;
                canSpawn = true;
            }
        }
    }
}
