using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private float cooldownInput;
    [SerializeField]
    private float durationInput;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private GameObject riggedPlayer;

    private float cooldown;
    private float duration;
    private int fixedDirection;
    private GameObject player;
    private Rigidbody2D playerRb;
    private Movement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fixedDirection = playerMovement.Direction;
                duration = durationInput;
                cooldown = cooldownInput;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        dash();
    }

    private void dash()
    {
        
        if(duration >= 0)
        {
            riggedPlayer.GetComponent<Animator>().SetBool("isDashing", true);
            duration -= Time.deltaTime;
            playerRb.velocity = new Vector2(fixedDirection * dashSpeed, playerRb.velocity.y);
        }
        else
        {
            riggedPlayer.GetComponent<Animator>().SetBool("isDashing", false);
        }
    }
}
