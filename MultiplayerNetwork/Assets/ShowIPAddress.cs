using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIPAddress : MonoBehaviour
{
    NetworkHandler networkHandler;

	// Use this for initialization
	void Start ()
    {
        networkHandler = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkHandler>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Text>().text = networkHandler.serverIP;
	}
}
