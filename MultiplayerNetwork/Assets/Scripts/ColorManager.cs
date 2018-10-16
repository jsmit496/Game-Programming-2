using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorManager : NetworkBehaviour
{
    [SyncVar]
    int colorID;

    [SyncVar]
    string colorName;

    public bool assignedAColor = false;

    public Material[] playerMaterials;
    public bool assigned = false;

    public int ColorID
    {
        get
        {
            return colorID;
        }
        set
        {
            if (isServer)
            {
                colorID = value;
            }
        }
    }

    private ChatHandler chatHandler;

    public string ColorName
    {
        get
        {
            return gameObject.GetComponent<Renderer>().sharedMaterial.name.Replace("matPlayer", string.Empty);
        }
    }

    // Use this for initialization
    void Start ()
    {
        chatHandler = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (assignedAColor == false)
        {
            AssignColor();
            gameObject.GetComponent<Renderer>().sharedMaterial = playerMaterials[ColorID];
        }
	}

    public void AssignColor()
    {
        for (int i = 0; i < playerMaterials.Length; i++)
        {
            assigned = false;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<ColorManager>().assignedAColor == true)
                {
                    if (playerMaterials[i] == playerMaterials[player.GetComponent<ColorManager>().colorID])
                    {
                        assigned = true;
                    }
                }
            }

            if (assigned == false)
            {
                ColorID = i;
                assignedAColor = true;
            }

        }
    }
}
