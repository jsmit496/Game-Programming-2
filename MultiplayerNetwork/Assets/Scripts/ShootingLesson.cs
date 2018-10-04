using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootingLesson : NetworkBehaviour
{
    public Transform shootPosition;
    public GameObject bullet;
    public float launchForce = 100;

    public int numberOfShotsFired;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            CmdFire();
        }
	}

    //Only server executes the code when useing [Command], also needs "Cmd" prefix
    [Command]
    void CmdFire()
    {
        //Only server will see this number increase
        numberOfShotsFired++;

        GameObject newBullet = GameObject.Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * launchForce);

        //Handle spawning on each client
        //In order to spawn it needs to be a prefab,have network identity, and add to registered spawnable prefabs on networkHandler
        NetworkServer.Spawn(newBullet);
    }

    //Will execute on the client
    [ClientRpc]
    void RpcShowHit()
    {
        print("I got hit!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //bool for if it is active on the server (if not active on server then do nothing)
        if (!isServer)
        {
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            RpcShowHit();
        }
    }
}
