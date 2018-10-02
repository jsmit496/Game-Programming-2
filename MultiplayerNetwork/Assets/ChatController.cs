using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour {

    public Text lblMessages;
    public InputField txtMessage;
    public Button btnSend;
    public Scrollbar scrollbar;

    public int numberOfLines = 5;

    private List<string> displayLines = new List<string>();
    public float scrollbarSize = 1;
    public int numMessages = 0;

    private NetworkHandler networkHandler;
    private ChatHandler chatHandler;

    //Implement OnValueChange so that whenever the value for the scrollbar changes it will display 5 messages based on its value in regards to the number of comments
    //Look at notes to recall the formulas and variables needed to accomplish this

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

        HandleScrollbar();
    }

    public void HandleScrollbar()
    {
        //As messages the number of messages increase change the scrollbar size so that it matches with the size you can see in regards to what you cant
        if (chatHandler.messages.Count <= 5)
        {
            scrollbarSize = 1;
        }
        else if (chatHandler.messages.Count > 5)
        {
            scrollbarSize = 5 / (float)chatHandler.messages.Count;
        }
        numMessages = chatHandler.messages.Count;
        scrollbar.size = scrollbarSize;

        //When moving the scrollwheel move 1 message up and down maxScrollbarValue / (totalMessages - totalMessagesToShow).
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            float percentScroll = 1 / (float)(chatHandler.messages.Count - 5);
            scrollbar.value += percentScroll;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            float percentScroll = 1 / (float)(chatHandler.messages.Count - 5);
            scrollbar.value -= percentScroll;
        }
    }

    public void btnSend_Click()
    {
        string message = txtMessage.text.Trim();
        if (message.Length > 0 && message.Length <= 100)
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

    public void Scrollbar_OnValueChanged()
    {
        //This will handle when the scrollbar's value changes and will change the messages you can see based on it
    }
}
