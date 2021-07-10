using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogInManager : MonoBehaviour
{
    [SerializeField]
    GameObject popOutLogIn;

    public string notifUname;
    public string notifPass;
    public GameObject inputFieldUname;
    public GameObject inputFieldPass;
    public GameObject displayNotif;
    

    void Start()
    {
        popOutLogIn.SetActive(false);
    }

    public void LogInGame()
    {
        notifUname = inputFieldUname.GetComponent<Text>().text;
        notifPass = inputFieldPass.GetComponent<Text>().text;
        displayNotif.GetComponent<Text>().text = notifUname + " and " + notifPass;
        popOutLogIn.SetActive(true);

        GameClient client = GameObject.Find("GameClient").GetComponent<GameClient>();
        client.ConnectToServer(notifUname, notifPass);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
