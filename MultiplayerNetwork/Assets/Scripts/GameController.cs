using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public bool gameOver = false;
    public int requiredScore = 20;
    public float motionScale = 90f;
    public float timerMax = 10f;
    public bool closeServer = false;

    public GameObject winner = null;

    private float timer;
    private NetworkHandler networkHandler;

	// Use this for initialization
	void Start ()
    {
        timer = timerMax;
        networkHandler = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkHandler>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        EndGame();
        if (gameOver)
        {
            CountDown();
        }
	}

    public void EndGame()
    {
        if (!gameOver)
        {
            return;
        }
        
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != winner)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerShoot>().enabled = false;
            }
        }

        foreach(GameObject playerCamera in GameObject.FindGameObjectsWithTag("PlayerCamera"))
        {
            if (playerCamera.transform.parent.gameObject != winner)
            {
                playerCamera.transform.position = winner.GetComponent<PlayerShoot>().endGamePosition.position;
                playerCamera.transform.rotation = winner.GetComponent<PlayerShoot>().endGamePosition.rotation;
            }
        }

        foreach(GameObject playerWeapon in GameObject.FindGameObjectsWithTag("PlayerWeapon"))
        {
            if (playerWeapon.transform.parent.parent.gameObject != winner)
            {
                playerWeapon.SetActive(false);
            }
        }

        winner.GetComponent<PlayerShoot>().endGameRotationPoint.Rotate(Vector3.up, motionScale * Time.deltaTime, Space.World);
    }

    public void CountDown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            networkHandler.ShutItDown();
        }
    }
}
