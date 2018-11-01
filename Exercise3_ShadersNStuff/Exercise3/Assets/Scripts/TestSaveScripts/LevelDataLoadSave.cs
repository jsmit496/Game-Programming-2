using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class LevelDataLoadSave : MonoBehaviour
{
    public GameObject sphereTemplate;
    public GameObject obstacleTemplate;

    private LevelData levelData = new LevelData();

    private LevelController levelController;

    // Use this for initialization
    void Start ()
    {
        //Find Multiple Files
        string[] fileNames = System.IO.Directory.GetFiles(".", "*.lvl");

        StringBuilder sb = new StringBuilder();
        foreach (string fileName in fileNames)
        {
            sb.AppendLine(fileName);
        }
        print(sb.ToString());

        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void LoadLevel(string levelName)
    {
        LevelData level = LevelData.LoadFromFile(levelName + ".lvl");

        //translation from level data into game object data
        levelController.levelName = level.levelName;

        //Ground plane
        float groundPlaneX = level.groundPlane.width;
        float groundPlaneY = level.groundPlane.length;
        Transform groundPlane = GameObject.FindGameObjectWithTag("GroundPlane").transform;
        groundPlane.localScale = new Vector3(groundPlaneX, groundPlaneY, 1);

        //Player FOV
        levelController.playerFov = level.PlayerFieldOfView;

        //Player Detection Distance
        levelController.playerDetectionDistance = level.playerDetectionDistance;

        //Obstacles
        foreach (GameObject existingObstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(existingObstacle);
        }
        foreach (ObstacleData obstacle in level.obstacles)
        {
            GameObject levelObstacle = GameObject.Instantiate(obstacleTemplate);
            levelObstacle.transform.position = obstacle.position;
            levelObstacle.transform.localEulerAngles = new Vector3(1, obstacle.yRotation, 1);
        }
        


        foreach (GameObject existingSphere in GameObject.FindGameObjectsWithTag("MovingSphere"))
        {
            Destroy(existingSphere);
        }

        foreach (MovingSphereData movingSphere in levelData.movingSpheres)
        {
            GameObject levelSphere = GameObject.Instantiate(sphereTemplate);
            levelSphere.transform.position = movingSphere.position;
            levelSphere.GetComponent<MoveCube>().moveSpeed = movingSphere.moveSpeed;
        }
    }

    private void SaveLevel(string levelName)
    {
        LevelData level = new LevelData();

        //translation from game object data into level data
        level.levelName = levelController.levelName;

        //Ground plane
        Transform groundPlane = GameObject.FindGameObjectWithTag("GroundPlane").transform;
        level.groundPlane.width = groundPlane.localScale.x;
        level.groundPlane.length = groundPlane.localScale.y;

        //Player FOV
        level.PlayerFieldOfView = levelController.playerFov;

        //Player Detection Distance
        level.playerDetectionDistance = levelController.playerDetectionDistance;

        //Obstacle (single)
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            ObstacleData newObstacle = new ObstacleData();
            newObstacle.position = obstacle.transform.position;
            newObstacle.yRotation = obstacle.transform.localEulerAngles.y;

            level.obstacles.Add(newObstacle);
        }

        foreach (GameObject movingSphereGameObject in GameObject.FindGameObjectsWithTag("MovingSphere"))
        {
            MovingSphereData currentMovingSphere = new MovingSphereData();
            currentMovingSphere.position = movingSphereGameObject.transform.position;
            currentMovingSphere.moveSpeed = movingSphereGameObject.GetComponent<MoveCube>().moveSpeed;

            levelData.movingSpheres.Add(currentMovingSphere);
        }

        level.SaveToFile(levelName + ".lvl");

    }

    private string loadLevelName = "default";
    private string saveLevelName = "default";

    private void OnGUI()
    {
        if (GUILayout.Button("Load Level"))
        {
            LoadLevel(loadLevelName);
        }
        GUILayout.Label("Level name to load");
        loadLevelName = GUILayout.TextField(loadLevelName);

        GUILayout.Label(string.Empty);
        GUILayout.Label(string.Empty);
        GUILayout.Label(string.Empty);

        if (GUILayout.Button("Save Level"))
        {
            SaveLevel(saveLevelName);
        }
        GUILayout.Label("Level name to save");
        saveLevelName = GUILayout.TextField(saveLevelName);
    }
}

[Serializable]
public class GroundPlane
{
    public float width;
    public float length;
}

[Serializable]
public class ObstacleData
{
    public Vector3 position;
    public float yRotation;
}

[Serializable]
public class LevelData
{
    public List<MovingSphereData> movingSpheres = new List<MovingSphereData>();

    //Item 1 - placeholder for level name
    public string levelName;
    public GroundPlane groundPlane = new GroundPlane();

    public float fieldOfView;
    public float playerDetectionDistance;

    public List<ObstacleData> obstacles = new List<ObstacleData>();

    //Item 2 - getter/setter for external objects to manipulate level name
    public string GetLevelName()
    {
        return levelName;
    }
    public void SetLevelName(string levelName)
    {
        this.levelName = levelName;
    }

    public float PlayerFieldOfView
    {
        get
        {
            return fieldOfView;
        }
        set
        {
            fieldOfView = value;
        }
    }

    public float PlayerDetectionDistance
    {
        get
        {
            return playerDetectionDistance;
        }
        set
        {
            playerDetectionDistance = value;
        }
    }

    public void SaveToFile(string fileName)
    {
        System.IO.File.WriteAllText(fileName, JsonUtility.ToJson(this, true));
        MonoBehaviour.print(System.IO.Directory.GetCurrentDirectory());
    }

    public static LevelData LoadFromFile(string fileName)
    {
        return JsonUtility.FromJson<LevelData>(System.IO.File.ReadAllText(fileName));
    }
}

[Serializable]
public class MovingSphereData
{
    public Vector3 position;
    public float moveSpeed;
}
