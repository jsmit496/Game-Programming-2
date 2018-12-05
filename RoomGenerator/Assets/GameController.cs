using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    LevelGrid levelGrid;

    GameObject dummyPlayer;

	// Use this for initialization
	void Start ()
    {
        levelGrid = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<LevelGrid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetPlayerPosition()
    {
        if (levelGrid.roomNodes != null)
        {
            foreach (Node n in levelGrid.roomNodes)
            {
                if (n.isStart)
                {
                    dummyPlayer = Instantiate(player);
                    dummyPlayer.GetComponent<PlayerMovement>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
                    dummyPlayer.transform.position = new Vector3(n.position.x, n.position.y + (player.transform.localScale.y), n.position.z);
                }
            }
        }
    }
}
