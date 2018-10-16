using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEndCameraRotation : MonoBehaviour
{
    public bool endGame = false;
    public float motionScale = 90f;

    public Transform endPositionPoint;
    public Transform endRotationPoint;
    public GameObject playerCamera;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (endGame)
        {
            RotateCamera();
        }
	}

    private void RotateCamera()
    {
        playerCamera.transform.position = endPositionPoint.position;
        playerCamera.transform.rotation = endPositionPoint.rotation;
        endRotationPoint.Rotate(Vector3.up, motionScale * Time.deltaTime, Space.World);
    }
}
