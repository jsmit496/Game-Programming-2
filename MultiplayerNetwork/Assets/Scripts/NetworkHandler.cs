using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkHandler : NetworkManager
{
    public Text joinGameText;
    public Text playerNameText;

    private string playerName;
    private ChatHandler chatHandler;

    public class ChatMessage : MessageBase
    {
        public string sender;
        public string message;
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerNameText.text.Trim().Length > 0)
        {
            playerName = playerNameText.text;
        }
        else
        {
            playerName = "PlayerWithNoName";
        }
        
	}

    //Setup for Host/Client
    public void SetupHost()
    {
        StartHost();
        RegisterServerListeners();
        RegisterClientListeners();
    }

    public void SetupClient()
    {
        if (joinGameText.text.Trim().Length > 0)
        {
            networkAddress = joinGameText.text;
        }
        else
        {
            networkAddress = "localhost";
        }
        
        StartClient();
        RegisterClientListeners();
    }

    //Handle Connect/Join
    public override void OnClientConnect(NetworkConnection netConnect)
    {
        //When client connects: set the chat
        chatHandler = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();
        client.Send(2001, new StringMessage(playerName));
    }

    public void OnOtherPlayerJoinedGame(NetworkMessage netMsg)
    {
        string playerName = netMsg.ReadMessage<StringMessage>().value;
        chatHandler.AnnouncePlayer(playerName);
    }

    //Handle Setting PlayerName
    public void OnNameAssigned(NetworkMessage netMsg)
    {
        string playerName = netMsg.ReadMessage<StringMessage>().value;
        chatHandler.SetLocalPlayerName(playerName);
    }

    public void OnPlayerNameRecieved(NetworkMessage netMsg)
    {
        string playerName = netMsg.ReadMessage<StringMessage>().value;
        playerName = chatHandler.SetPlayerName(playerName, netMsg.conn.connectionId);
        NetworkServer.SendToClient(netMsg.conn.connectionId, 2002, new StringMessage(playerName));
        NetworkServer.SendToAll(2003, new StringMessage(playerName));
    }

    //Handle Chat Messages Being Sent/Received
    public void SendChatMessage(string message)
    {
        client.Send(3000, new StringMessage(message));
    }

    public void OnPlayerSendChatMessage(NetworkMessage netMsg)
    {
        string message = netMsg.ReadMessage<StringMessage>().value.Trim();
        if (message.Length > 100)
        {
            message = message.Substring(0, 100).Trim();
        }
        string senderName = chatHandler.GetNameByConnectionId(netMsg.conn.connectionId);

        ChatMessage chatMessage = new ChatMessage();
        chatMessage.sender = senderName;
        chatMessage.message = message;
        NetworkServer.SendToAll(3001, chatMessage);
    }

    public void OnChatMessageReceieved(NetworkMessage netMsg)
    {
        ChatMessage received = netMsg.ReadMessage<ChatMessage>();
        chatHandler.OnChatMessageReceived(received.sender, received.message);
    }

    //Handle RegisterHandler's
    private void RegisterServerListeners()
    {
        NetworkServer.RegisterHandler(2001, OnPlayerNameRecieved);
        NetworkServer.RegisterHandler(3000, OnPlayerSendChatMessage);
    }

    private void RegisterClientListeners()
    {
        client.RegisterHandler(2002, OnNameAssigned);
        client.RegisterHandler(2003, OnOtherPlayerJoinedGame);
        client.RegisterHandler(3001, OnChatMessageReceieved);
    }
}
