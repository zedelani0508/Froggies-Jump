using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public ScoreManager sm;

    Text timerText;
    public static float timeleft;
    public GameObject gameOver;
    public GameObject finishGame;

    void Start()
    {
        timeleft = 60;
        timerText = GetComponent<Text>();
        gameOver.SetActive(false);
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        if(timeleft < 0)
        {
            timeleft = 0;
            // GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            // Player.timeFinish = timeleft;
            // sm.AddScore(new Score(Player.playerUsername, Player.timeFinish));
            // gameOver.SetActive(true);
            finishGame.SetActive(false);
        }
        timerText.text = "Time Left: " + Mathf.Round(timeleft);
    }
}
