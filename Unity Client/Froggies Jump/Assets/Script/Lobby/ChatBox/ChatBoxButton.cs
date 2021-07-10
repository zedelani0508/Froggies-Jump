using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxButton : MonoBehaviour
{
    [SerializeField]
    GameObject chatBox;

    bool isOpen = false;

    public void Start()
    {
        chatBox.SetActive(false);
    }

    public void OpenCloseChatBox()
    {
        if(!isOpen)
        {
            chatBox.SetActive(true);
            isOpen = true;
        } else
        {
            chatBox.SetActive(false);
            isOpen = false;
        }

    }
}
