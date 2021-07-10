using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickLevel : MonoBehaviour
{
    [SerializeField]
    GameObject popOutWindow;

    [SerializeField]
    GameObject waitWindow;

    private void Start()
    {
        popOutWindow.SetActive(false);
        waitWindow.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
            popOutWindow.SetActive(true);
        }
    }

    public void SelectLevel1()
    {
        popOutWindow.SetActive(false);
        SceneManager.LoadScene("Game1");
        Debug.Log("Player choose level!");
        waitWindow.SetActive(true);
    }
    
    public void SelectLevel2()
    {
        popOutWindow.SetActive(false);

        Debug.Log("Player choose level!");
        waitWindow.SetActive(true);

    }
    
    public void SelectLevel3()
    {
        popOutWindow.SetActive(false);

        Debug.Log("Player choose level!");
        waitWindow.SetActive(true);

    }

    public void ClosePanel1()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        popOutWindow.SetActive(false);
    }
    public void ClosePanel2()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        popOutWindow.SetActive(false);
    }
    
    public void ClosePanel3()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        popOutWindow.SetActive(false);
    }
}
