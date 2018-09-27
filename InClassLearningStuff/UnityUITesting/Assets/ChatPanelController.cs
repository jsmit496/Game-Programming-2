using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanelController : MonoBehaviour
{
    public Text lblMessages;
    public InputField txtMessage;
    public Button btnSend;

    public int numberOfLines = 5;

    private List<string> displayLines = new List<string>();

	// Use this for initialization
	void Start ()
    {
        lblMessages.text = string.Empty;

        txtMessage.ActivateInputField();
        txtMessage.Select();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Return) && !txtMessage.IsActive())
        {
            txtMessage.ActivateInputField();
            txtMessage.Select();
        }
	}

    public void btnSend_Click()
    {
        DoLocalSubmissionOnly();
    }

    private void DoLocalSubmissionOnly()
    {
        string message = txtMessage.text.Trim();
        if (message.Length > 0)
        {
            displayLines.Add(message);
            if (displayLines.Count > numberOfLines)
            {
                displayLines.RemoveAt(0);
            }

            lblMessages.text = string.Empty;
            for (int i = 0; i < displayLines.Count; i++)
            {
                lblMessages.text += displayLines[i];
                if (i < displayLines.Count - 1)
                {
                    lblMessages.text += "\n";
                }
            }

            txtMessage.text = string.Empty;
        }
        else if (message.Length <= 0)
        {
            txtMessage.text = string.Empty;
        }
    }

    public void txtMessage_EndEdit()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            btnSend_Click();
        }
    }

}
