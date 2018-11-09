using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text levelNameText;
    public Text saveFileText;
    public Text saveLevelNameText;
    public Text loadLevelText;

    private LevelController levelController;
    private LevelDataLoadSave levelData;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        levelData = GameObject.FindGameObjectWithTag("LevelData").GetComponent<LevelDataLoadSave>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        levelNameText.text = levelController.levelName;
	}

    public void SaveLevelButton()
    {
        levelData.SaveLevel("TestLevelSave");
    }

}
