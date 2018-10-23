using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineGameStatus : MonoBehaviour
{
    public bool gameOver = false;

    [HideInInspector]
    public int numItemsFound;

    [HideInInspector]
    public int numItemsToFind;

    private List<GameObject> itemsToFind = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        itemsToFind.AddRange(GameObject.FindGameObjectsWithTag("Pickup"));
        foreach (GameObject gbj in itemsToFind)
        {
            numItemsToFind++;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver)
        {
            gameObject.GetComponent<Analytics>().FoundAllObjects(true);
        }
	}

    private void OnApplicationQuit()
    {
        gameObject.GetComponent<Analytics>().FoundAllObjects(false);
    }
}
