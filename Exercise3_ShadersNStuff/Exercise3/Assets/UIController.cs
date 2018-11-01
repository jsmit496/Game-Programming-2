using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text lblLevelName;

    private LevelController levelController;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        lblLevelName.text = levelController.levelName;
	}

}
