using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkHandler : NetworkManager
{
    public string playerName;

    public Text joinGameText;
    public Text playerNameText;

    //add display place for chat

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void SetupHost()
    {
        StartHost();
        NetworkServer.RegisterHandler(2001, OnPlayerNameRecieved);
        client.RegisterHandler(2004, OnOtherPlayerJoinedGame);
    }

    public void SetupClient()
    {
        networkAddress = joinGameText.text;
        StartClient();
        client.RegisterHandler(2003, OnOtherPlayerJoinedGame);
    }

    public override void OnClientConnect(NetworkConnection netConnect)
    {
        //When client connects: set the chat
        client.Send(2001, new StringMessage(playerName));
    }

    public void OnOtherPlayerJoinedGame(NetworkMessage netMsg)
    {
        string playerName = netMsg.ReadMessage<StringMessage>().value;
        print(playerName + "joined");
        //display name in chat
    }

    public void OnPlayerNameRecieved(NetworkMessage netMsg)
    {
        string playerName = netMsg.ReadMessage<StringMessage>().value;
        NetworkServer.SendToAll(2003, new StringMessage(playerName));
    }
}
