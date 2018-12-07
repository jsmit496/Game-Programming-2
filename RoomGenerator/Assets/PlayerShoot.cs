using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPosition;
    public float bulletSpeed;

    public int numArrows = 20;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && numArrows > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject dummyBullet;
        dummyBullet = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed * Time.deltaTime);
        numArrows--;
    }
}
