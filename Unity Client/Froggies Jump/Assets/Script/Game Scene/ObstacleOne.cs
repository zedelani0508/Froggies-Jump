using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleOne : MonoBehaviour
{
    
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    public float raycastLength;
 

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, raycastLength, LayerMask.GetMask("Ground"));
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            // col.gameObject.GetComponent<Transform>().Translate(2, 0, 0);
            // col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            // StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        
        yield return new WaitForSeconds(5);
        Debug.Log("Wait Done!");
        GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }
}
