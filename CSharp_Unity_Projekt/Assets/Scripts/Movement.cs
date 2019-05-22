using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int jumpCountInput;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public bool isGrounded = false;
    [SerializeField]
    private float moveSmooth;
    [SerializeField]
    private float airMoveSmooth;
    [SerializeField]
    [Range(0, 1)]
    private float jumpForceReduced;
    [SerializeField]
    private float groundedTimerInput;
    [SerializeField]
    private float keyPressedTimerInput;

    private float groundedTimer;
    private float keyPressedTimer;
    private int jumpCount;
    private Rigidbody2D rb;
    private float moveX;
    private float airForce = 1.5f;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping;

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

        groundedTimer -= Time.deltaTime;
        keyPressedTimer -= Time.deltaTime;

        move();
        jump();
        flip();
    }

    private void move()
    {

        if (isGrounded)
        {
            if (FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").isPlaying != true && moveX != 0)
            {
                FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Play();
            }
            else if (moveX == 0)
            {
                FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Stop();
            }
        }
        Vector3 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmooth);
    }

    private void jump()
    {       
        if(isGrounded)
            groundedTimer = groundedTimerInput;

        if (Input.GetKeyDown(KeyCode.W))
            keyPressedTimer = keyPressedTimerInput;

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForceReduced);
            }
        }

        if ((groundedTimer > 0) && (keyPressedTimer > 0))
        {
            groundedTimer = 0;
            keyPressedTimer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void flip()
    {
        if (moveX < 0)
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (moveX > 0)
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if(moveX == 0)
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                this.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            else
                this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
