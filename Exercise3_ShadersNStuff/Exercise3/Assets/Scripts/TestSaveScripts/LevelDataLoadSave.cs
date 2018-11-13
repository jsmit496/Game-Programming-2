using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class LevelDataLoadSave : MonoBehaviour
{
    public GameObject[] obstacleTemplates;
    public GameObject[] targetableTemplates;
    public GameObject[] distractionTemplates;

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

    public void LoadLevel(string levelName)
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

        //Level Player Distance
        levelController.playerPosition = level.PlayerPosition;

        //Obstacles
        foreach (GameObject existingObstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(existingObstacle);
        }
        foreach (ObstacleData obstacle in level.obstacles)
        {
            foreach (GameObject obj in obstacleTemplates)
            {
                if (obstacle.objectType == obj.name)
                {
                    GameObject levelObstacle = Instantiate(obj);
                    levelObstacle.transform.position = obstacle.position;
                    levelObstacle.transform.localEulerAngles = new Vector3(0, obstacle.yRotation, 0);
                }
            }
        }

        foreach (GameObject existingTarget in GameObject.FindGameObjectsWithTag("Pickup"))
        {
            Destroy(existingTarget);
        }
        foreach (TargetableData targetObject in level.targets)
        {
            foreach (GameObject obj in targetableTemplates)
            {
                if (targetObject.objectType == obj.name)
                {
                    GameObject levelTarget = Instantiate(obj);
                    levelTarget.transform.position = targetObject.position;
                    levelTarget.transform.localEulerAngles = new Vector3(targetObject.xRotation, targetObject.yRotation, targetObject.zRotation);
                    levelTarget.GetComponent<EditObjectGlow>().GlowColor = targetObject.glowColor;
                }
            }
        }

        foreach (GameObject exisitingDistraction in GameObject.FindGameObjectsWithTag("Distraction"))
        {
            Destroy(exisitingDistraction);
        }
        foreach (DistractionData distractionObject in level.distractions)
        {
            foreach (GameObject obj in distractionTemplates)
            {
                if (distractionObject.objectType == obj.name)
                {
                    GameObject levelDistraction = Instantiate(obj);
                    levelDistraction.transform.position = distractionObject.position;
                    levelDistraction.transform.localEulerAngles = new Vector3(distractionObject.xRotation, distractionObject.yRotation, distractionObject.zRotation);
                }
            }
        }

        /*foreach (GameObject existingSphere in GameObject.FindGameObjectsWithTag("MovingSphere"))
        {
            Destroy(existingSphere);
        }

        foreach (MovingSphereData movingSphere in levelData.movingSpheres)
        {
            GameObject levelSphere = GameObject.Instantiate(sphereTemplate);
            levelSphere.transform.position = movingSphere.position;
            levelSphere.GetComponent<MoveCube>().moveSpeed = movingSphere.moveSpeed;
        }*/
    }

    public void SaveLevel(string levelName)
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
        level.PlayerDetectionDistance = levelController.playerDetectionDistance;

        //Player Position
        level.PlayerPosition = levelController.playerPosition;

        //Obstacle
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            ObstacleData newObstacle = new ObstacleData();
            foreach (GameObject obj in obstacleTemplates)
            {
                if (obstacle.name.Contains(obj.name))
                {
                    newObstacle.objectType = obj.name;
                }
            }
            newObstacle.position = obstacle.transform.position;
            newObstacle.yRotation = obstacle.transform.localEulerAngles.y;

            level.obstacles.Add(newObstacle);
        }

        foreach (GameObject targetObjects in GameObject.FindGameObjectsWithTag("Pickup"))
        {
            TargetableData newTargetObject = new TargetableData();
            foreach (GameObject obj in targetableTemplates)
            {
                if (targetObjects.name.Contains(obj.name))
                {
                    newTargetObject.objectType = obj.name;
                }
            }
            newTargetObject.position = targetObjects.transform.position;
            newTargetObject.xRotation = targetObjects.transform.localEulerAngles.x;
            newTargetObject.yRotation = targetObjects.transform.localEulerAngles.y;
            newTargetObject.zRotation = targetObjects.transform.localEulerAngles.z;
            newTargetObject.glowColor = targetObjects.GetComponent<EditObjectGlow>().GlowColor;

            level.targets.Add(newTargetObject);
        }

        foreach (GameObject distractionObject in GameObject.FindGameObjectsWithTag("Distraction"))
        {
            DistractionData newDistractionObject = new DistractionData();
            foreach (GameObject obj in distractionTemplates)
            {
                if (distractionObject.name.Contains(obj.name))
                {
                    newDistractionObject.objectType = obj.name;
                }
            }
            newDistractionObject.position = distractionObject.transform.position;
            newDistractionObject.xRotation = distractionObject.transform.localEulerAngles.x;
            newDistractionObject.yRotation = distractionObject.transform.localEulerAngles.y;
            newDistractionObject.zRotation = distractionObject.transform.localEulerAngles.z;

            level.distractions.Add(newDistractionObject);
        }

        level.SaveToFile(levelName + ".lvl");

    }

    private string loadLevelName = "default";
    private string saveLevelName = "default";

    /*private void OnGUI()
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
    }*/
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
    public string objectType;
    public Vector3 position;
    public float yRotation;
}

[Serializable]
public class TargetableData
{
    public string objectType;
    public Vector3 position;
    public float xRotation;
    public float yRotation;
    public float zRotation;
    public Color glowColor;
}

[Serializable]
public class DistractionData
{
    public string objectType;
    public Vector3 position;
    public float xRotation;
    public float yRotation;
    public float zRotation;
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
    public Vector3 playerPosition;

    public List<ObstacleData> obstacles = new List<ObstacleData>();
    public List<TargetableData> targets = new List<TargetableData>();
    public List<DistractionData> distractions = new List<DistractionData>();

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

    public Vector3 PlayerPosition
    {
        get
        {
            return playerPosition;
        }
        set
        {
            playerPosition = value;
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
