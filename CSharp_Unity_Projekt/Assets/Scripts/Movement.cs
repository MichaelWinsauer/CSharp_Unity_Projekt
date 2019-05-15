﻿using System;
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
    public AudioSource footstep; 

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
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
        else if (isGrounded)
            jumpCount = jumpCountInput;
    }
}
