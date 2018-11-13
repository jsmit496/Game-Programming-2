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

    public Dropdown obstacleDropdown;
    public Dropdown targetDropdown;
    public Dropdown distractionDropdown;

    public InputField groundPlaneLength;
    public InputField groundPlaneWidth;

    public GameObject levelMenu1;
    public GameObject levelMenu2;
    public GameObject levelMenu3;

    private LevelController levelController;
    private LevelDataLoadSave levelData;
    private EditorMovement editorMovement;

    float xPos, yPos, zPos, xRot, yRot, zRot;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        levelData = GameObject.FindGameObjectWithTag("LevelData").GetComponent<LevelDataLoadSave>();
        editorMovement = GameObject.FindGameObjectWithTag("LevelEditor").GetComponent<EditorMovement>();
        DropdownOptions(obstacleDropdown, levelData.obstacleTemplates);
        DropdownOptions(targetDropdown, levelData.targetableTemplates);
        DropdownOptions(distractionDropdown, levelData.distractionTemplates);
    }
	
	// Update is called once per frame
	void Update ()
    {
        levelNameText.text = levelController.levelName;
    }

    public void SaveLevelButton1()
    {
        //levelData.SaveLevel("TestLevelSave");
        levelMenu1.SetActive(false);
        levelMenu2.SetActive(true);
    }

    public void LoadLevelButton1()
    {
        //levelData.LoadLevel("TestLevelSave");
        levelMenu1.SetActive(false);
        levelMenu2.SetActive(true);
    }

    public void SaveLevelButton2()
    {
        levelController.levelName = saveLevelNameText.text;
        levelData.SaveLevel(saveFileText.text);
    }

    public void LoadLevelButton2()
    {
        levelData.LoadLevel(loadLevelText.text);
    }

    public void BackButton()
    {
        if (levelMenu2.activeSelf || levelMenu3.activeSelf)
        {
            levelMenu2.SetActive(false);
            levelMenu3.SetActive(false);
            levelMenu1.SetActive(true);
        }
    }

    public void CreateButton(Dropdown dropdown)
    {
        GameObject createObject = null;
        if (createObject == null)
        {
            foreach (var obj in levelData.obstacleTemplates)
            {
                if (dropdown.options[dropdown.value].text == obj.name)
                {
                    createObject = Instantiate(obj);
                }
            }
        }
        
        if (createObject == null)
        {
            foreach (var obj in levelData.targetableTemplates)
            {
                if (dropdown.options[dropdown.value].text == obj.name)
                {
                    createObject = Instantiate(obj);
                }
            }
        }

        if (createObject == null)
        {
            foreach (var obj in levelData.distractionTemplates)
            {
                if (dropdown.options[dropdown.value].text == obj.name)
                {
                    createObject = Instantiate(obj);
                }
            }
        }
    }

    public void ApplyObjectOptions()
    {
        if (editorMovement.activeObjectOptionsMenu != null)
        {
            GameObject[] inputFields;
            if (editorMovement.activeObjectOptionsMenu == editorMovement.obstacleUI)
            {
                inputFields = editorMovement.obstacleUI.GetComponent<InputFieldList>().menuInputFields;
                //Set GameObject's position and rotation in y
                foreach (GameObject obj in inputFields)
                {
                    if (obj.tag == "XPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionX, editorMovement.maximumPositionX);
                    }
                    else if (obj.tag == "YPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionY, editorMovement.maximumPositionY);
                    }
                    else if (obj.tag == "ZPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionZ, editorMovement.maximumPositionZ);
                    }
                    else if (obj.tag == "YRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationY, editorMovement.maximumRotationY);
                    }
                }
                editorMovement.selectedObject.transform.position = new Vector3(xPos, editorMovement.selectedObject.transform.position.y, zPos);
                editorMovement.selectedObject.transform.localEulerAngles = new Vector3(editorMovement.selectedObject.transform.localEulerAngles.x, yRot, editorMovement.selectedObject.transform.localEulerAngles.z);
            }
            else if (editorMovement.activeObjectOptionsMenu == editorMovement.targetUI)
            {
                //Set GameObject's position and rotation
                inputFields = editorMovement.obstacleUI.GetComponent<InputFieldList>().menuInputFields;
                //Set GameObject's position and rotation in y
                foreach (GameObject obj in inputFields)
                {
                    if (obj.tag == "XPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionX, editorMovement.maximumPositionX);
                    }
                    else if (obj.tag == "YPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionY, editorMovement.maximumPositionY);
                    }
                    else if (obj.tag == "ZPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionZ, editorMovement.maximumPositionZ);
                    }
                    else if (obj.tag == "XRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationX, editorMovement.maximumRotationX);
                    }
                    else if (obj.tag == "YRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationY, editorMovement.maximumRotationY);
                    }
                    else if (obj.tag == "ZRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationZ, editorMovement.maximumRotationZ);
                    }
                }
                editorMovement.selectedObject.transform.position = new Vector3(xPos, editorMovement.selectedObject.transform.position.y, zPos);
                editorMovement.selectedObject.transform.localEulerAngles = new Vector3(xRot, yRot, yRot);
            }
            else if (editorMovement.activeObjectOptionsMenu == editorMovement.distractionUI)
            {
                //Set GameObject's position and rotation
                inputFields = editorMovement.obstacleUI.GetComponent<InputFieldList>().menuInputFields;
                //Set GameObject's position and rotation in y
                foreach (GameObject obj in inputFields)
                {
                    if (obj.tag == "XPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionX, editorMovement.maximumPositionX);
                    }
                    else if (obj.tag == "YPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionY, editorMovement.maximumPositionY);
                    }
                    else if (obj.tag == "ZPositionInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumPositionZ, editorMovement.maximumPositionZ);
                    }
                    else if (obj.tag == "XRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationX, editorMovement.maximumRotationX);
                    }
                    else if (obj.tag == "YRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationY, editorMovement.maximumRotationY);
                    }
                    else if (obj.tag == "ZRotationInput")
                    {
                        ChangeObjectPosition(obj, editorMovement.minimumRotationZ, editorMovement.maximumRotationZ);
                    }
                }
                editorMovement.selectedObject.transform.position = new Vector3(xPos, editorMovement.selectedObject.transform.position.y, zPos);
                editorMovement.selectedObject.transform.localEulerAngles = new Vector3(xRot, yRot, yRot);
            }
            else if (editorMovement.activeObjectOptionsMenu == editorMovement.groundPlaneUI)
            {
                //Set GameObject's scale in x && y
                inputFields = editorMovement.obstacleUI.GetComponent<InputFieldList>().menuInputFields;
                float length = 5;
                float width = 5;
                //Set GameObject's position and rotation in y
                foreach (GameObject obj in inputFields)
                {
                    if (obj.tag == "LengthInput")
                    {
                        if (float.TryParse(obj.GetComponent<InputField>().text, out length))
                        {
                            if (length < 5)
                            {
                                length = 5;
                            }
                            else if (length > 20)
                            {
                                length = 20;
                            }
                        }
                        else
                        {
                            length = 5;
                        }
                    }
                    else if (obj.tag == "WidthInput")
                    {
                        if (float.TryParse(obj.GetComponent<InputField>().text, out width))
                        {
                            if (width < 5)
                            {
                                width = 5;
                            }
                            else if (width > 20)
                            {
                                width = 20;
                            }
                        }
                        else
                        {
                            width = 5;
                        }
                    }
                }
                editorMovement.transform.localScale = new Vector3(length, width, editorMovement.transform.localScale.z);
            }
        }
    }

    public void DropdownOptions(Dropdown dropdown, GameObject[] optionsArray)
    {
        List<string> options = new List<string>();
        foreach (GameObject obj in optionsArray)
        {
            options.Add(obj.name);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void ChangeObjectPosition(GameObject obj, float min, float max)
    {
        if (float.TryParse(obj.GetComponent<InputField>().text, out xPos))
        {
            if (xPos < min)
            {
                xPos = min;
            }
            else if (xPos > max)
            {
                xPos = max;
            }
        }
        else if (obj.GetComponent<InputField>().text == null)
        {
            xPos = min;
        }
        else
        {
            xPos = min;
        }
    }

}
