using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 5f;

    Rigidbody2D rb;

    PlayerController target;
    Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        //Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            // this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            // col.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            // StartCoroutine(Pause());
            
            
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(7);
        Debug.Log("Wait Done!");
        GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Destroy(gameObject);
    }
}
