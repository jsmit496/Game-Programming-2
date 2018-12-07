using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth = 10f;

    [HideInInspector]
    public float currentHealth;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
