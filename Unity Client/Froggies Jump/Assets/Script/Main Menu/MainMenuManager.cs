using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject accountPanel;
    
    [SerializeField]
    GameObject settingsPanel;
    
    [SerializeField]
    GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    public void AccountConfig()
    {
        accountPanel.SetActive(true);
    }
    
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        accountPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }

}
