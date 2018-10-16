using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public float bulletSpeed = 4.0f;
    //public float singleFireWaitTime = 4.0f;
    //public float shotgunWaitTime = 4.0f;
    //public float rapidFireWaitTime = 4.0f;
    //public float explosionWaitTime = 4.0f;
    //public int numBulletSpread = 1;
    //public float bulletSpreadAngle;
    //public bool canAimDownSight = true;
    //public string weaponType = "Single Fire"; //Single Fire, Rapid Fire, Explosion, Shotgun
    //public float uniqueWeaponLimit = 10f;

    public Transform shootPositon;
    public GameObject bullet;
    public Transform endGamePosition;
    public Transform endGameRotationPoint;

    //private float shootingWaitTimeCount = 0;
    //private bool didShoot = false;
    //private float uniqueWeaponLimitCount = 0;
    //private bool isUniqueEquiped = false;

    //private Quaternion spreadRotation;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Mouse0) && !didShoot)
        {
            CmdShootBullet();
            didShoot = true;
        }
        if (didShoot)
        {
            CmdWaitToShoot();
        }*/

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdBasicShoot();
        }
    }
    /*
    [Command]
    public void CmdShootBullet()
    {
        GameObject dummyBullet;
        if (weaponType == "Single Fire")
        {
            dummyBullet = Instantiate(bullet, shootPositon.position, shootPositon.rotation);
            dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);
            shootingWaitTimeCount = singleFireWaitTime;
            dummyBullet.GetComponent<BulletController>().owningPlayer = this;
            NetworkServer.Spawn(dummyBullet);
        }
        else if (weaponType == "Rapid Fire")
        {
            dummyBullet = Instantiate(bullet, shootPositon.position, shootPositon.rotation);
            dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);
            shootingWaitTimeCount = rapidFireWaitTime;
            dummyBullet.GetComponent<BulletController>().owningPlayer = this;
            NetworkServer.Spawn(dummyBullet);
        }
        else if (weaponType == "Explosion")
        {
            //Shoot a single bullet that when it collides with anything it explodes (will need a lot of work for networking)

        }
        else if (weaponType == "Shotgun")
        {
            for (int i = 0; i < numBulletSpread; i++)
            {
                dummyBullet = Instantiate(bullet, shootPositon.position, shootPositon.rotation);
                spreadRotation = Random.rotation;
                dummyBullet.transform.rotation = Quaternion.RotateTowards(dummyBullet.transform.rotation, spreadRotation, bulletSpreadAngle);
                dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);
                dummyBullet.GetComponent<BulletController>().owningPlayer = this;
                NetworkServer.Spawn(dummyBullet);
            }
            shootingWaitTimeCount = shotgunWaitTime;
        }

        didShoot = true;
    }*/

    [Command]
    public void CmdBasicShoot()
    {
        GameObject dummyBullet;
        dummyBullet = Instantiate(bullet, shootPositon.position, shootPositon.rotation);
        dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);
        dummyBullet.GetComponent<BulletController>().owningPlayer = this;
        NetworkServer.Spawn(dummyBullet);
    }

    /*
    public void CmdWaitToShoot()
    {
        if (shootingWaitTimeCount > 0)
        {
            shootingWaitTimeCount -= Time.deltaTime;
        }
        else if (shootingWaitTimeCount <= 0)
        {
            if (weaponType == "Single Fire")
            {
                shootingWaitTimeCount = singleFireWaitTime;
            }
            else if (weaponType == "Rapid Fire")
            {
                shootingWaitTimeCount = rapidFireWaitTime;
            }
            else if (weaponType == "Explosion")
            {
                shootingWaitTimeCount = explosionWaitTime;
            }
            else if (weaponType == "Shotgun")
            {
                shootingWaitTimeCount = shotgunWaitTime;
            }

            didShoot = false;
        }
    }

    public void SetUniqueWeapon()
    {
        if (weaponType == "Rapid Fire" || weaponType == "Explosion" || weaponType == "Shotgun")
        {
            //Timer is displayed and when timer == 0 we set weaponType back to "Single Fire"

        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
        {
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            PlayerShoot shooter = collision.gameObject.GetComponent<BulletController>().owningPlayer;
            shooter.GetComponent<PlayerScore>().Score++;
            if (shooter.GetComponent<PlayerScore>().Score >= gameController.requiredScore && gameController.winner == null)
            {
                gameController.winner = shooter.gameObject;
                gameController.gameOver = true;
            }
        }
    }
}
