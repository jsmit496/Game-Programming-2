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
        if (n.neighbors.ContainsKey("left") && n.neighbors["left"].isRoom)
        {
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = leftSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = leftSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = leftSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = leftSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("right") && n.neighbors["right"].isRoom)
        {
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = rightSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = rightSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = rightSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = rightSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("bottom") && n.neighbors["bottom"].isRoom)
        {
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = bottomSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = bottomSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = bottomSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = bottomSpawnPoint.transform.localEulerAngles;
        }
        if (n.neighbors.ContainsKey("top") && n.neighbors["top"].isRoom)
        {
            dummyWall = Instantiate(doorway);
            dummyWall.transform.position = topSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = topSpawnPoint.transform.localEulerAngles;
        }
        else
        {
            dummyWall = Instantiate(wall);
            dummyWall.transform.position = topSpawnPoint.transform.position;
            dummyWall.transform.localEulerAngles = topSpawnPoint.transform.localEulerAngles;
        }
    }
}
