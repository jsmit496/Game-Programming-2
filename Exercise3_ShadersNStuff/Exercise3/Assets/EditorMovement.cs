using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public float motionScale = 90f;
    public float maxAngle = 90f;

    public GameObject playerCamera;

    public GameObject obstacleUI;
    public GameObject targetUI;
    public GameObject distractionUI;
    public GameObject groundPlaneUI;

    public Transform groundPlane;

    private CursorLockMode cursorMode;

    [HideInInspector]
    public float minimumPositionX, minimumPositionY, minimumPositionZ;

    [HideInInspector]
    public float maximumPositionX, maximumPositionY, maximumPositionZ;

    [HideInInspector]
    public float minimumRotationX, minimumRotationY, minimumRotationZ;

    [HideInInspector]
    public float maximumRotationX, maximumRotationY, maximumRotationZ;

    [HideInInspector]
    public GameObject selectedObject;

    [HideInInspector]
    public GameObject activeObjectOptionsMenu;

    void SetCursorState()
    {
        Cursor.lockState = cursorMode;
        Cursor.visible = (CursorLockMode.Locked != cursorMode);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        BasicFirstPersonControls();
        SetMinMax();
        CheckRaycastFromMouse();
	}

    private void BasicFirstPersonControls()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection += transform.up;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection += -transform.up;
        }

        transform.position += moveDirection.normalized * speed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX > 0 || mouseY > 0)
        {
            transform.Rotate(Vector3.up, mouseX * motionScale * Time.deltaTime, Space.World);

            CameraUpDownMovement();
        }
        if (mouseX < 0 || mouseY < 0)
        {
            transform.Rotate(Vector3.down, -mouseX * motionScale * Time.deltaTime, Space.World);

            CameraUpDownMovement();
        }

        //This will also deal with opening the menu and what not
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorMode != CursorLockMode.Locked)
            {
                cursorMode = CursorLockMode.Locked;
            }
            else if (cursorMode == CursorLockMode.Locked)
            {
                cursorMode = CursorLockMode.None;
            }
            SetCursorState();
        }
    }

    public void CameraUpDownMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseY > 0)
        {
            playerCamera.transform.Rotate(-transform.right, mouseY * motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(transform.forward, playerCamera.transform.forward) > maxAngle)
            {
                playerCamera.transform.forward = transform.forward;
                playerCamera.transform.Rotate(-transform.right, maxAngle, Space.World);
            }
        }
        if (mouseY < 0)
        {
            playerCamera.transform.Rotate(transform.right, -mouseY * motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(transform.forward, playerCamera.transform.forward) > maxAngle)
            {
                playerCamera.transform.forward = transform.forward;
                playerCamera.transform.Rotate(transform.right, maxAngle, Space.World);
            }
        }
    }

    public void CheckRaycastFromMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                selectedObject = hit.collider.gameObject;
                //print(string.Format("Hit {0}", hit.collider.gameObject.name));
                if (hit.collider.tag == "Pickup")
                {
                    //Set UI to Pickup UI
                    obstacleUI.SetActive(false);
                    distractionUI.SetActive(false);
                    groundPlaneUI.SetActive(false);
                    targetUI.SetActive(true);
                    activeObjectOptionsMenu = targetUI;
                }
                else if (hit.collider.tag == "Obstacle")
                {
                    //Set UI to Obstacle UI
                    distractionUI.SetActive(false);
                    groundPlaneUI.SetActive(false);
                    targetUI.SetActive(false);
                    obstacleUI.SetActive(true);
                    activeObjectOptionsMenu = obstacleUI;
                }
                else if (hit.collider.tag == "Distraction")
                {
                    //Set UI to Distraction UI
                    groundPlaneUI.SetActive(false);
                    targetUI.SetActive(false);
                    obstacleUI.SetActive(false);
                    distractionUI.SetActive(true);
                    activeObjectOptionsMenu = distractionUI;
                }
                else if (hit.collider.tag == "GroundPlane")
                {
                    //Set UI to GroundPlane UI
                    distractionUI.SetActive(false);
                    targetUI.SetActive(false);
                    obstacleUI.SetActive(false);
                    groundPlaneUI.SetActive(true);
                    activeObjectOptionsMenu = groundPlaneUI;
                }
                else
                {
                    distractionUI.SetActive(false);
                    targetUI.SetActive(false);
                    obstacleUI.SetActive(false);
                    groundPlaneUI.SetActive(false);
                    activeObjectOptionsMenu = null;
                }
            }
        }
    }

    public void SetMinMax()
    {
        minimumPositionX = groundPlane.localScale.x / -2;
        maximumPositionX = groundPlane.localScale.x / 2;

        minimumPositionY = 0;
        maximumPositionY = 5;

        minimumPositionZ = groundPlane.localScale.z / -2;
        maximumPositionZ = groundPlane.localScale.z / 2;

        minimumRotationX = 0;
        maximumRotationX = 360;

        minimumRotationY = 0;
        maximumRotationX = 360;

        minimumRotationZ = 0;
        maximumRotationZ = 360;
    }
}
