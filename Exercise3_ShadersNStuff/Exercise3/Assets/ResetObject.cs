using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetObject : MonoBehaviour
{
    public bool objectFound = false;

    [HideInInspector]
    public bool reset = false;

    [HideInInspector]
    public Quaternion originalRotation;
    [HideInInspector]
    public Vector3 originalPosition;
    [HideInInspector]
    public Vector3 originalScale;

    private bool countObject = false;

    private InspectItems inspectItems;
    private Analytics analytics;
    private DetermineGameStatus DGS;

    // Use this for initialization
    void Start ()
    {
        inspectItems = GameObject.FindGameObjectWithTag("Camera").GetComponent<InspectItems>();
        analytics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Analytics>();
        DGS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DetermineGameStatus>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ResetObjectToOriginalPosition();
        if (objectFound)
        {
            if (!countObject)
            {
                GetComponent<EditObjectGlow>().GlowColor = Color.black;
                analytics.FindObject(SceneManager.GetActiveScene().name, gameObject.name);
                DGS.numItemsFound++;
                countObject = true;
            }
        }
	}

    public void ResetObjectToOriginalPosition()
    {
        if (reset)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, inspectItems.shrinkSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, originalPosition, inspectItems.itemMovementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, inspectItems.resetRotationSpeed * Time.deltaTime);
            if (transform.localScale == originalScale && transform.position == originalPosition && transform.rotation == originalRotation)
            {
                reset = false;
            }
            objectFound = true;
        }
    }
}
