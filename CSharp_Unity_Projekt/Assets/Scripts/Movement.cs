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

    private int jumpCount;
    private Rigidbody2D rb;
    private float moveX;
    private float airForce = 2;
    
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
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(moveX * moveSpeed / airForce, rb.velocity.y);
            //rb.AddForce(new Vector2(moveX * moveSpeed, 0));
        }
            
    }

    private void jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.W)) && jumpCount > 1) //Keine Ahnung, warum ich hier grade 1 machen muss, damit ich 2 Sprünge habe...
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpCount--;
        }
        else if (isGrounded)
            jumpCount = jumpCountInput;
    }
}
