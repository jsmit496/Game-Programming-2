using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHandler : MonoBehaviour
{
    public string chatType = "General Chat";
    public List<string> messages = new List<string>();

    public GameObject chatController;
    public GameObject chatToggle;

    private Dictionary<int, string> namesByConnectionId = new Dictionary<int, string>();
    private Dictionary<string, int> connectionIdsByName = new Dictionary<string, int>();

    private string localPlayerName;

	// Use this for initialization
	void Start ()
    {
        ChatController chatControl = chatController.GetComponent<ChatController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (chatToggle.activeSelf == true)
            {
                chatToggle.SetActive(false);
            }
            else if (chatToggle.activeSelf == false)
            {
                chatToggle.SetActive(true);
            }
        }
    }

    public void OnChatMessageReceived(string sender, string message)
    {
        messages.Add(string.Format("[{0}] {1}: {2}", chatType, sender, message));
    }

    public string GetNameByConnectionId(int connectionId)
    {
        return namesByConnectionId[connectionId];
    }

    public void SetLocalPlayerName(string playerName)
    {
        localPlayerName = playerName;
    }

    public void AnnouncePlayer(string playerName)
    {
        messages.Add(string.Format("User {0} joined the server", playerName));
    }

    private string UniqueName(string playerName, int connectionId)
    {
        if (namesByConnectionId.ContainsKey(connectionId) && namesByConnectionId[connectionId] == playerName)
        {
            return playerName;
        }

        int suffix = 0;
        while (connectionIdsByName.ContainsKey(playerName))
        {
            ++suffix;
            string suffixString = suffix.ToString();
            playerName += suffixString;
        }

        return playerName;
    }

    internal string SetPlayerName(string playerName, int connectionId)
    {
        playerName = UniqueName(playerName, connectionId);

        if (namesByConnectionId.ContainsKey(connectionId))
        {
            connectionIdsByName.Remove(namesByConnectionId[connectionId]);
            namesByConnectionId.Remove(connectionId);
        }

        connectionIdsByName.Add(playerName, connectionId);
        namesByConnectionId.Add(connectionId, playerName);

        return playerName;
    }

}
