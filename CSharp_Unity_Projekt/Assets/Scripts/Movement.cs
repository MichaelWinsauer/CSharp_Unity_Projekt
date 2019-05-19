using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public int jumpCountInput = 2;
    public float jumpForce;
    public bool isGrounded = false;
    public float moveSmooth = 0.05f;
    public float airMoveSmooth = 0.2f;
    public AudioSource footstep; 

    public int jumpCount;
    private Rigidbody2D rb;
    private float moveX;
    private float airForce = 1.5f;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = jumpCountInput - 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (moveX < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else 
            GetComponent<SpriteRenderer>().flipX = false;

        if (Input.GetKeyDown(KeyCode.W))
            isJumping = true;
        else
            isJumping = false;
        jump();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        
        if (isGrounded)
        {
            if (footstep.isPlaying != true && moveX != 0)
            {
                footstep.Play();
            }
            else if(moveX == 0)
            {
                footstep.Stop();
            }
            Vector3 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmooth);
        }
        else
        {
            Vector3 targetVelocity = new Vector2(moveX * moveSpeed / airForce, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, airMoveSmooth);
        }
    }

    private void jump()
    {       
        if (isJumping && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
        else if (isGrounded)
            jumpCount = jumpCountInput - 1;
    }
}
