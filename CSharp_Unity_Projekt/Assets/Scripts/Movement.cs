using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public int jumpCountInput;
    public float jumpForce;
    public bool isGrounded = false;
    public float moveSmooth = 0.05f;
    public float airMoveSmooth = 0.2f;

    private int jumpCount;
    private Rigidbody2D rb;
    private float moveX;
    private float airForce = 1.5f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = jumpCountInput;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        move();

        jump();
    }

    private void move()
    {
        
        if (isGrounded)
        {
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
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && jumpCount > 1) //Keine Ahnung, warum ich hier grade 1 machen muss, damit ich 2 Sprünge habe...
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
        else if (isGrounded)
            jumpCount = jumpCountInput;
    }
}
