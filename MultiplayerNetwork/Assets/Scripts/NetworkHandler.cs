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
    public bool isServer;
    public string serverIP;

    private string playerName;
    private ChatHandler chatHandler;
    private ScoreboardController scoreboardController;
    public bool playerJoined = false;

    internal void RegisterScoreboard(ScoreboardController scoreBoard)
    {
        scoreboardController = scoreBoard;
    }

    public class ChatMessage : MessageBase
    {
        public string sender;
        public string message;
    }

	// Use this for initialization
	void Start ()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string hostName = System.Net.Dns.GetHostName();
        foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(hostName).AddressList)
        {
            sb.AppendLine(ip.ToString());

            if (IsValidIPAddress(ip.ToString()))
            {
                serverIP = ip.ToString();
            }
        }
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
        string ipAddress = joinGameText.text.Trim();
        if (!IsValidIPAddress(ipAddress))
        {
            joinGameText.text = string.Empty;
        }
        else
        {
            serverIP = joinGameText.text;
            networkAddress = joinGameText.text;
            StartClient();
            RegisterClientListeners();
        }
    }

    public void ShutItDown()
    {
        StopClient();
        StopHost();
        StopServer();
    }

    //Handle Connect/Join
    public override void OnClientConnect(NetworkConnection netConnect)
    {
        //When client connects: set the chat
        chatHandler = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();
        client.Send(2001, new StringMessage(playerName));
        isServer = false;
        playerJoined = true;
    }

    public override void OnStartServer()
    {
        isServer = true;
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

    //Handle IP Connection
    private bool IsValidIPAddress(string ipAddress)
    {
        string[] components = ipAddress.Split('.');
        if (components.Length != 4)
        {
            return false;
        }

        uint a;
        uint b;
        uint c;
        uint d;

        if (!uint.TryParse(components[0], out a))
        {
            return false;
        }
        if (!uint.TryParse(components[1], out b))
        {
            return false;
        }
        if (!uint.TryParse(components[2], out c))
        {
            return false;
        }
        if (!uint.TryParse(components[3], out d))
        {
            return false;
        }

        if (a + b == 0)
        {
            return false;
        }

        return true;
    }
}
