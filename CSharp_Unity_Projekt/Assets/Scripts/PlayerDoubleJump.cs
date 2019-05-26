using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    [SerializeField]
    private float cooldownInput;
    [SerializeField]
    private float durationInput;
    [SerializeField]
    private float jumpForce;

    private GameObject player;
    private Movement playerMovement;
    private Rigidbody2D playerRb;
    private float cooldown;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
            if(!playerMovement.IsGrounded)
                if(Input.GetKeyDown(KeyCode.W))
                {
                    cooldown = cooldownInput;
                    duration = durationInput;
                }

        doubleJump();
    }

    private void doubleJump()
    {
        
        if(duration >= 0)
        {
            duration -= Time.deltaTime;
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
        else
        {
        }
    }
}
