using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<PlayerShoot>().numArrows += collision.gameObject.GetComponent<GenerateChest>().numArrows;
                Destroy(collision.gameObject);
            }
        }
    }
}
