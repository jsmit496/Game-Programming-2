using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerCamera;

    public float speed = 4.0f;
    public float rotationSpeed = 4.0f;
    public float cameraMovementSpeed = 4.0f;
    public float cameraDistanceY = 4.0f;

    public LayerMask detectMouse;

    private bool setCameraPosition = false;
    private Vector3 desiredPosition;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        PlayerRotation();
        SetCameraPosition();
	}

    void Movement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }
        transform.position += moveDirection * Time.deltaTime * speed;
    }

    void PlayerRotation()
    {
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectMouse))
        {
            Vector3 targetDir = hit.point - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0f);
            Quaternion newRotation = Quaternion.LookRotation(newDir);
            newRotation.x = 0;
            newRotation.z = 0;
            transform.rotation = newRotation;
        }

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, detectMouse))
        {
            print(string.Format("hitInfo position: {0}; hitInfo rotation: {1}", hitInfo.transform.position, hitInfo.transform.rotation));
            Vector3 targetDir = hitInfo.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0f);
            Quaternion newRotation = Quaternion.LookRotation(newDir);
            newRotation.x = 0;
            newRotation.z = 0;
            transform.rotation = newRotation;
        }*/
    }

    void SetCameraPosition()
    {
        if (setCameraPosition)
        {
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, desiredPosition, cameraMovementSpeed * Time.deltaTime);
            if (playerCamera.transform.position == desiredPosition)
            {
                setCameraPosition = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Room")
        {
            setCameraPosition = true;
            desiredPosition = new Vector3(collision.gameObject.transform.position.x, cameraDistanceY, collision.gameObject.transform.position.z);
        }
    }
}
