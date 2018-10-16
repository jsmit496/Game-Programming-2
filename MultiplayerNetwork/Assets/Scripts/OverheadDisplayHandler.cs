using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OverheadDisplayHandler : NetworkBehaviour
{
    public float fillPercentage;

    public Text playerNameTxt;
    public Text playerPointsTxt;
    public Image playerPointsProgressBar;

    private ChatHandler chatHandler;
    private PlayerScore playerScore;
    private GameController gameController;

    private List<GameObject> overheadsToMove = new List<GameObject>();
    private List<GameObject> personalOverheads = new List<GameObject>();

    //Does not function 100% correctly (have idea for fix but not enough time)

	// Use this for initialization
	void Start ()
    {
        chatHandler = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();
        playerScore = GetComponent<PlayerScore>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleOverheadCreation();
        RpcRotateAllOverheads();
    }

    private void HandleOverheadCreation()
    {
        playerNameTxt.text = chatHandler.localPlayerName;
        playerPointsTxt.text = string.Format("Points: {0}", playerScore.Score);
        fillPercentage = (float)playerScore.Score / gameController.requiredScore;
        playerPointsProgressBar.fillAmount = fillPercentage;
    }

    [ClientRpc]
    public void RpcRotateAllOverheads()
    {
        foreach (GameObject overhead in GameObject.FindGameObjectsWithTag("Overhead"))
        {
            overheadsToMove.Add(overhead);
        }

        foreach (GameObject overhead in overheadsToMove)
        {
            GameObject dummyOverhead = Instantiate(overhead);
            personalOverheads.Add(dummyOverhead);
        }

        foreach (GameObject overhead in personalOverheads)
        {
            if (overhead.transform.parent.gameObject != this.gameObject)
            {
                overhead.transform.LookAt(gameObject.transform);
            }
        }
    }
}
