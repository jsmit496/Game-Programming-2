using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWalls : MonoBehaviour
{
    public GameObject wall;
    public GameObject doorway;

    public GameObject leftSpawnPoint, rightSpawnPoint, bottomSpawnPoint, topSpawnPoint;

    LevelGrid levelGrid;

    GameObject dummyWall;

    float wallScaleX, wallScaleZ;

    [HideInInspector]
    public Node sourceNode;

    // Use this for initialization
    void Start()
    {
        levelGrid = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<LevelGrid>();
        SetWalls();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWalls()
    {
        Node n = sourceNode;
        float x, y, z;
        if (n.neighbors.ContainsKey("left") && n.neighbors["left"].isRoom)
        {
            x = leftSpawnPoint.transform.position.x;
            z = leftSpawnPoint.transform.position.z;
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = leftSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            x = leftSpawnPoint.transform.position.x;
            z = leftSpawnPoint.transform.position.z;
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = leftSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("right") && n.neighbors["right"].isRoom)
        {
            x = rightSpawnPoint.transform.position.x;
            z = rightSpawnPoint.transform.position.z;
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = rightSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            x = rightSpawnPoint.transform.position.x;
            z = rightSpawnPoint.transform.position.z;
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = rightSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("bottom") && n.neighbors["bottom"].isRoom)
        {
            x = bottomSpawnPoint.transform.position.x;
            z = bottomSpawnPoint.transform.position.z;
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y/2, z);
            dummyWall.transform.localEulerAngles = bottomSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            x = bottomSpawnPoint.transform.position.x;
            z = bottomSpawnPoint.transform.position.z;
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = bottomSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("top") && n.neighbors["top"].isRoom)
        {
            x = topSpawnPoint.transform.position.x;
            z = topSpawnPoint.transform.position.z;
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = topSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            x = topSpawnPoint.transform.position.x;
            z = topSpawnPoint.transform.position.z;
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = new Vector3(x, dummyWall.transform.localScale.y / 2, z);
            dummyWall.transform.localEulerAngles = topSpawnPoint.transform.localEulerAngles;
        }
    }
}
