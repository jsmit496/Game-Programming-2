using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPosition;
    public float bulletSpeed;

    public int numArrows = 20;

    private Text arrowCounter;

    // Use this for initialization
    void Start ()
    {
        arrowCounter = GameObject.FindGameObjectWithTag("ArrowCounter").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        arrowCounter.text = numArrows.ToString();
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
