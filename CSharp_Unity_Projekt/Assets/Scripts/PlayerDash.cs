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

    private float cooldown;
    private float duration;
    private int fixedDirection;
    private GameObject player;
    private Rigidbody2D playerRb;
    private Movement playerMovement;
    private float defaultGravity;

    private States dashState;

    //Referenzen der SpielerObjekten
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<Movement>();
        defaultGravity = playerRb.gravityScale;
        dashState = States.Ready;
    }

    void Update()
    {
        switch(dashState)
        {
            case States.Ready:
                if (Input.GetButtonDown("Dash"))
                {
                    fixedDirection = playerMovement.Direction;
                    duration = durationInput;
                    dashState = States.Dashing;
                }
                break;

            case States.Dashing:
                if (duration >= 0 && !GetComponent<PlayerHealth>().IsDead)
                {
                    playerRb.gravityScale = 0;
                    duration -= Time.deltaTime;
                    playerRb.velocity = new Vector2(fixedDirection * dashSpeed, 0);
                }
                else
                {
                    playerRb.gravityScale = defaultGravity;
                    cooldown = cooldownInput;
                    dashState = States.Cooldown;
                }
                break;

            case States.Cooldown:
                if (cooldown >= 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else
                {
                    dashState = States.Ready;
                }
                break;
        }
    }

    private enum States
    {
        Ready,
        Dashing,
        Cooldown,
    }

}
