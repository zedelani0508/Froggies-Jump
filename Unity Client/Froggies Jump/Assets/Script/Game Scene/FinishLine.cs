using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public ScoreManager sm;
    public GameObject finishObject;
    public GameObject waitingRoom;

    public Text email;

    Transform waitPos;

    GameClient client;

    private void Start()
    {
        waitPos = waitingRoom.GetComponent<Transform>();
        client = GameObject.Find("GameClient").GetComponent<GameClient>();
        email.text = client.emailplayer;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            col.GetComponent<Transform>().transform.position = waitPos.transform.position;
            client.WinnerMoveToWaitingRoom(waitPos.position.x, waitPos.position.y);
        }
    }

    public void SetWinnerName(string playername)
    {
        finishObject.SetActive(true);
        Text winnerName = GameObject.Find("Name").GetComponent<Text>();
        winnerName.text = playername;
    }
}
