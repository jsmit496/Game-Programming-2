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

    private ChatHandler chatController;

	// Use this for initialization
	void Start ()
    {
		if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            chatController = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (chatController.localPlayerName != this.playerName)
            {
                CmdSetName(chatController.localPlayerName);
            }
        }
	}

    [Command]
    public void CmdSetName(string playerName)
    {
        this.playerName = playerName;
    }
}
