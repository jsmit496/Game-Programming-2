using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour {

    public Text lblMessages;
    public InputField txtMessage;
    public Button btnSend;

    public int numberOfLines = 5;

    private List<string> displayLines = new List<string>();

    private NetworkHandler networkHandler;
    private ChatHandler chatHandler;

    // Use this for initialization
    void Start()
    {

        lblMessages.text = string.Empty;

        txtMessage.ActivateInputField();
        txtMessage.Select();



        GameObject networkController = GameObject.FindGameObjectWithTag("NetworkController");
        if (networkController != null)
        {
            networkHandler = networkController.GetComponent<NetworkHandler>();
        }

        chatHandler = GameObject.FindGameObjectWithTag("ChatSystem").GetComponent<ChatHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (networkHandler != null)
        {
            displayLines.Clear();
            for (int i = chatHandler.messages.Count - 1; i >= 0 && displayLines.Count < numberOfLines; i--)
            {
                displayLines.Add(chatHandler.messages[i]);
            }
            displayLines.Reverse();
            RefreshMessageDisplay();
        }
    }


    public void btnSend_Click()
    {

        string message = txtMessage.text.Trim();
        if (message.Length > 0)
        {
            if (networkHandler == null)
            {
                displayLines.Add(message);
                if (displayLines.Count > numberOfLines)
                {
                    displayLines.RemoveAt(0);
                }

                RefreshMessageDisplay();
            }
            else
            {
                networkHandler.SendChatMessage(message);
            }

            txtMessage.text = string.Empty;
        }

        txtMessage.ActivateInputField();

    }

    public void RefreshMessageDisplay()
    {
        lblMessages.text = string.Empty;
        for (int i = 0; i < displayLines.Count; i++)
        {
            lblMessages.text += displayLines[i];
            if (i < displayLines.Count - 1) lblMessages.text += "\n";
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
