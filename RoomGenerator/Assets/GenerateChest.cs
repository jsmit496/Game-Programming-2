using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChest : MonoBehaviour
{
    public GameObject chest;

    public int numArrows = 20;

    LevelGrid levelGrid;
    GameObject dummyChest;

	// Use this for initialization
	void Start ()
    {
        levelGrid = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<LevelGrid>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void SetChest()
    {
        foreach (Node n in levelGrid.roomNodes)
        {
            if (n.containsChest)
            {
                foreach (RoomNode rn in n.attachedRoom.GetComponent<RoomGrid>().tileNodes)
                {
                    if (!rn.isBlocked)
                    {
                        Vector3 desiredPosition = new Vector3(rn.position.x, chest.transform.position.y, rn.position.y);
                        dummyChest = Instantiate(chest, desiredPosition, chest.transform.rotation);
                    }
                }
            }
        }
    }
}
