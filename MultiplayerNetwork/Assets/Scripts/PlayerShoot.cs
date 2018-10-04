using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public float bulletSpeed = 4.0f;
    public float bulletSpreadAngle;
    public bool canAimDownSight = true;

    public Transform shootPositon;
    public GameObject bullet;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot();
        }
        
	}

    [Command]
    void CmdShoot()
    {
        GameObject dummyBullet = Instantiate(bullet, shootPositon.position, shootPositon.rotation);
        dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);

        NetworkServer.Spawn(dummyBullet);
    }
}
