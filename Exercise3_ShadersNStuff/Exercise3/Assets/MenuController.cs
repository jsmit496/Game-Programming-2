using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject[] menuOption1;
    GameObject[] menuOption2;

    private void Start()
    {
        menuOption1 = GameObject.FindGameObjectsWithTag("MenuOption1");
        menuOption2 = GameObject.FindGameObjectsWithTag("MenuOption2");

        foreach (GameObject obj in menuOption2)
        {
            obj.SetActive(false);
        }
    }

    public void EnableMenuObjects(string tagName)
    {
        HandleObjects(true, tagName);
    }

    public void DisableMenuObjects(string tagName)
    {
        HandleObjects(false, tagName);
    }

    public void HandleObjects(bool enable, string tagName)
    {
        //Determine based on tag what to show in menu
        GameObject[] objectsToHandle = null;
        if (tagName == "MenuOption1")
        {
            objectsToHandle = menuOption1;
        }
        else if (tagName == "MenuOption2")
        {
            objectsToHandle = menuOption2;
        }

        foreach (GameObject obj in objectsToHandle)
        {
            obj.SetActive(enable);
        }
    }
}
