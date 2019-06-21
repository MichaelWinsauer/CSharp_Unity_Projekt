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
    private GameObject dashStartParticles;
    [SerializeField]
    private GameObject dashDurationParticles;

    private float cooldown;
    private float duration;
    private int fixedDirection;
    private GameObject player;
    private Rigidbody2D playerRb;
    private Movement playerMovement;
    private float defaultGravity;
    private GameObject dashStart;
    private bool canDash;

    private States dashState;

    public bool CanDash { get => canDash; set => canDash = value; }

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
                if (Input.GetButtonDown("Dash") && canDash && !player.GetComponent<PlayerHealth>().IsDead)
                {
                    dashStart = Instantiate(dashStartParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(.15f, .6f);
                    fixedDirection = playerMovement.Direction;
                    duration = durationInput;
                    dashState = States.Dashing;
                }
                break;

            case States.Dashing:
                if (duration >= 0 && !GetComponent<PlayerHealth>().IsDead)
                {
                    //dashStart.transform.position = transform.position;
                    GameObject dashParticles = Instantiate(dashDurationParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                    ParticleSystem.ShapeModule shape = dashParticles.GetComponentInChildren<ParticleSystem>().shape;
                    shape.rotation = new Vector3(0, 90 * -fixedDirection, 0);
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
