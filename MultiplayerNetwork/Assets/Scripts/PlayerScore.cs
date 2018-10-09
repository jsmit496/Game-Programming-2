using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour
{
    //Broadcast information contained in the [SyncVar] to all clients
    [SyncVar]
    public int score = 0;

    [SyncVar]
    public string playerName = string.Empty;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (isServer)
            {
                score = value;
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
