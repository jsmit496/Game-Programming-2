using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWalls : MonoBehaviour
{
    public GameObject wall;

    LevelGrid levelGrid;

	// Use this for initialization
	void Start ()
    {
        levelGrid = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<LevelGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetWalls()
    {

    }
}
