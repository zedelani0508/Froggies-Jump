using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLain : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;

    public Transform groundDetector;
    bool isGrounded;
    public LayerMask whatIsGround;

    int extraJump;

    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerJump();
        FlipTrigger();
        DetectGround();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal2");

        Vector3 movement = new Vector3(x * speed, rb.velocity.y, 0f);
        rb.velocity = movement;
    }

    void FlipTrigger()
    {
        if (rb.velocity.x < 0 && facingRight)
        {
            FlipPlayer();
        }
        else if (rb.velocity.x > 0 && !facingRight)
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
        if (isGrounded)
        {
            extraJump = 1;
        }

        if (Input.GetButtonDown("Jump2") && extraJump > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            extraJump--;
        }
        else if (Input.GetButtonDown("Jump2") && extraJump == 0 && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            extraJump--;
        }
    }

    void DetectGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, 0.1f, whatIsGround);
    }
}
