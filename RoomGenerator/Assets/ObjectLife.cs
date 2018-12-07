using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{
    public float lifeTime = 5.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        EndOfLife();
	}

    void EndOfLife()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        else if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
    }
}
