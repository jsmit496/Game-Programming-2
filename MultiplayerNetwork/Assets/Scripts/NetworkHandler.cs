using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour
{
    public Text joinGameText;
    public Text playerNameText;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HostGame()
    {
        GetComponent<NetworkManager>().StartHost();
    }

    public void JoinGame()
    {
        GetComponent<NetworkManager>().networkAddress = joinGameText.text;
        GetComponent<NetworkManager>().StartClient();
    }
}
