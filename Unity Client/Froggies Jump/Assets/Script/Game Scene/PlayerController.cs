using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;

    public Transform groundDetector;
    bool isGrounded;
    public LayerMask whatIsGround;

    int extraJump;

    bool facingRight = true;

    GameClient client;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        client = GameObject.Find("GameClient").GetComponent<GameClient>();
    }
    void Update()
    {
        PlayerJump();
        // FlipTrigger();
        DetectGround();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // Vector3 movement = new Vector3(x * speed, rb.velocity.y, 0f);
        // rb.velocity = movement;
        // if (x > 0) {
        //     client.Move(1, rb.velocity.y, rb.position.x, rb.position.y);
        // } else if (x < 0) {
        //     client.Move(-1, rb.velocity.y, rb.position.x, rb.position.y);
        // } else {
        //     client.Move(0, rb.velocity.y, rb.position.x, rb.position.y);
        // }

        client.Move(x, rb.velocity.y, rb.position.x, rb.position.y);
}

    void FlipTrigger()
    {
        if (rb.velocity.x < 0 && facingRight)
        {
            FlipPlayer();
        } else if (rb.velocity.x > 0 && !facingRight)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void PlayerJump()
    {
        if(isGrounded)
        {
            extraJump = 1;
        }

        if(Input.GetButtonDown("Jump") && extraJump > 0)
        {
            client.Move(rb.velocity.x, jumpForce, rb.position.x, rb.position.y);
            // rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            extraJump--;

        } else if (Input.GetButtonDown("Jump") && extraJump == 0 && isGrounded)
        {
            // rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            client.Move(rb.velocity.x, jumpForce, rb.position.x, rb.position.y);
            extraJump--;
        }
    }

    void DetectGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, 0.1f, whatIsGround);
    }
}
