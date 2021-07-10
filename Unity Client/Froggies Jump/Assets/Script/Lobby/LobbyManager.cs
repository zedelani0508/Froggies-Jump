using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public int pickCounter;
    public int pick1;
    public int pick2;
    public int pick3;

    private void Update()
    {
        if(pickCounter >= 1)
        {
            if(pick1 >= 1)
            {
                SceneManager.LoadScene("Game1");
            }
            
            if(pick2 >= 1)
            {
                SceneManager.LoadScene("Game1");
            }
            
            if(pick3 >= 1)
            {
                SceneManager.LoadScene("Game1");
            }
        }
    }
}
