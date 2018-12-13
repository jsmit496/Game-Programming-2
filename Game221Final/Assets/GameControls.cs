using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour {

    public GameObject blackMoku, whiteMoku;

    public bool isBlacksTurn = true;

    public GameObject[,] stones = new GameObject[19, 19];

	// Use this for initialization
	void Start () {

        GameObject[] existingStones = GameObject.FindGameObjectsWithTag("Moku");
        for (int i = 0; i < existingStones.Length; i++)
        {
            Vector3 existingStoneLocation = existingStones[i].transform.position;
            stones[Mathf.RoundToInt(existingStoneLocation.x), Mathf.RoundToInt(existingStoneLocation.y)] = existingStones[i];
        }

	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int bx = Mathf.RoundToInt(mousePosition.x), by = Mathf.RoundToInt(mousePosition.y);

            if (bx >= 0 && bx <= 18 && by >= 0 && by <= 18 && stones[bx, by] == null)
            {
                GameObject newMoku = GameObject.Instantiate(isBlacksTurn ? blackMoku : whiteMoku);
                newMoku.transform.position = new Vector3(bx, by, 0);

                stones[bx, by] = newMoku;

                RemoveCapturedStones();

                isBlacksTurn = !isBlacksTurn;
            }
        }
	}

    private void RemoveCapturedStones()
    {
        // TODO: Fill out this function.
        // Any stones that are now surrounded because of the placement of the new stone
        // should be removed.

        // You do NOT need to worry about scoring - just remove any stones that are captured.

        List<GameObject> allWhiteMoku = new List<GameObject>();
        List<GameObject> allBlackMoku = new List<GameObject>();

        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 18; y++)
            {
                if (stones[x,y] != null)
                {
                    if (stones[x, y].name.Contains("Black Moku"))
                    {
                        allBlackMoku.Add(stones[x, y]);
                    }
                    else if (stones[x, y].name.Contains("White Moku"))
                    {
                        allWhiteMoku.Add(stones[x, y]);
                    }
                }
            }
        }

        if (isBlacksTurn)
        {
            foreach (GameObject obj in allWhiteMoku)
            {
                List<GameObject> checkedMoku = new List<GameObject>();
                List<GameObject> uncheckedMoku = new List<GameObject>();
                uncheckedMoku.Add(obj);
                while (uncheckedMoku.Count > 0)
                {
                    GameObject currentMoku = uncheckedMoku[0];
                    int x = (int)currentMoku.transform.position.x;
                    int y = (int)currentMoku.transform.position.y;
                    if (x - 1 >= 0 && stones[x - 1, y] != null && !uncheckedMoku.Contains(stones[x - 1, y]) && !checkedMoku.Contains(stones[x - 1, y]) && stones[x - 1, y].name.Contains("White Moku"))
                    {
                        //Check moku to left
                        uncheckedMoku.Add(stones[x - 1, y]);
                    }
                    if (x + 1 <= 18 && stones[x + 1, y] != null && !uncheckedMoku.Contains(stones[x + 1, y]) && !checkedMoku.Contains(stones[x + 1, y]) && stones[x + 1, y].name.Contains("White Moku"))
                    {
                        //Check moku to right
                        uncheckedMoku.Add(stones[x + 1, y]);
                    }
                    if (y - 1 >= 0 && stones[x, y - 1] != null && !uncheckedMoku.Contains(stones[x, y - 1]) && !checkedMoku.Contains(stones[x, y - 1]) && stones[x, y - 1].name.Contains("White Moku"))
                    {
                        //Check moku below
                        uncheckedMoku.Add(stones[x, y - 1]);
                    }
                    if (y + 1 <= 18 && stones[x, y + 1] != null && !uncheckedMoku.Contains(stones[x, y + 1]) && !checkedMoku.Contains(stones[x, y + 1]) && stones[x, y + 1].name.Contains("White Moku"))
                    {
                        //Check moku above
                        uncheckedMoku.Add(stones[x, y + 1]);
                    }
                    checkedMoku.Add(currentMoku);
                    uncheckedMoku.Remove(currentMoku);
                }
                print(checkedMoku.Count);
                bool hasEmptySide = false;
                foreach (GameObject moku in checkedMoku)
                {
                    int x = (int)moku.transform.position.x;
                    int y = (int)moku.transform.position.y;
                    if (x - 1 >= 0 && stones[x - 1, y] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (x + 1 <= 18 && stones[x + 1, y] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (y - 1 >= 0 && stones[x, y - 1] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (y + 1 <= 18 && stones[x, y + 1] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                }
                if (!hasEmptySide)
                {
                    foreach (GameObject moku in checkedMoku)
                    {
                        int x = (int)moku.transform.position.x;
                        int y = (int)moku.transform.position.y;
                        stones[x, y] = null;
                        Destroy(moku);
                    }
                }
            }
        }
        else if (!isBlacksTurn)
        {
            foreach (GameObject obj in allBlackMoku)
            {
                List<GameObject> checkedMoku = new List<GameObject>();
                List<GameObject> uncheckedMoku = new List<GameObject>();
                uncheckedMoku.Add(obj);
                while (uncheckedMoku.Count > 0)
                {
                    GameObject currentMoku = uncheckedMoku[0];
                    int x = (int)currentMoku.transform.position.x;
                    int y = (int)currentMoku.transform.position.y;
                    if (x - 1 >= 0 && stones[x - 1, y] != null && !uncheckedMoku.Contains(stones[x - 1, y]) && !checkedMoku.Contains(stones[x - 1, y]) && stones[x - 1, y].name.Contains("Black Moku"))
                    {
                        //Check moku to left
                        uncheckedMoku.Add(stones[x - 1, y]);
                    }
                    if (x + 1 <= 18 && stones[x + 1, y] != null && !uncheckedMoku.Contains(stones[x + 1, y]) && !checkedMoku.Contains(stones[x + 1, y]) && stones[x + 1, y].name.Contains("Black Moku"))
                    {
                        //Check moku to right
                        uncheckedMoku.Add(stones[x + 1, y]);
                    }
                    if (y - 1 >= 0 && stones[x, y - 1] != null && !uncheckedMoku.Contains(stones[x, y - 1]) && !checkedMoku.Contains(stones[x, y - 1]) && stones[x, y - 1].name.Contains("Black Moku"))
                    {
                        //Check moku below
                        uncheckedMoku.Add(stones[x, y - 1]);
                    }
                    if (y + 1 <= 18 && stones[x, y + 1] != null && !uncheckedMoku.Contains(stones[x, y + 1]) && !checkedMoku.Contains(stones[x, y + 1]) && stones[x, y + 1].name.Contains("Black Moku"))
                    {
                        //Check moku above
                        uncheckedMoku.Add(stones[x, y + 1]);
                    }
                    checkedMoku.Add(currentMoku);
                    uncheckedMoku.Remove(currentMoku);
                }

                bool hasEmptySide = false;
                foreach (GameObject moku in checkedMoku)
                {
                    int x = (int)moku.transform.position.x;
                    int y = (int)moku.transform.position.y;
                    if (x - 1 >= 0 && stones[x - 1, y] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (x + 1 <= 18 && stones[x + 1, y] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (y - 1 >= 0 && stones[x, y - 1] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                    if (y + 1 <= 18 && stones[x, y + 1] == null && !hasEmptySide)
                    {
                        hasEmptySide = true;
                    }
                }
                if (!hasEmptySide)
                {
                    foreach (GameObject moku in checkedMoku)
                    {
                        int x = (int)moku.transform.position.x;
                        int y = (int)moku.transform.position.y;
                        stones[x, y] = null;
                        Destroy(moku);
                    }
                }
            }
        }
    }
}
