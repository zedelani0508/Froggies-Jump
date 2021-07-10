using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendChat : MonoBehaviour
{
    public string chatText;
    public GameObject inputField;
    public GameObject chatDisplay;

    public void ChatSend()
    {
        chatText = inputField.GetComponent<Text>().text;
        chatDisplay.GetComponent<Text>().text = chatText;
        inputField.GetComponent<Text>().text = " ";
    }
}
