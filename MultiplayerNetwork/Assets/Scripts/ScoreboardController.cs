using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkHandler>().RegisterScoreboard(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        StringBuilder sb = new StringBuilder();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            sb.AppendFormat("{0}: {1}\n", player.GetComponent<PlayerScore>().playerName, player.GetComponent<PlayerScore>().Score);
        }

        GetComponent<Text>().text = sb.ToString();
	}
}
