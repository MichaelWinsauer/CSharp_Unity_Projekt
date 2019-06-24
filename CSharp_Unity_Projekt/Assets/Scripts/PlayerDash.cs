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
    private bool vertical;

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
                if(!GameData.options.UseXbox)
                {
                    if (Input.GetButtonDown("Dash") && canDash && !player.GetComponent<PlayerHealth>().IsDead)
                    {
                        dashStart = Instantiate(dashStartParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(.15f, .6f);
                        fixedDirection = playerMovement.Direction;
                        duration = durationInput;
                        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .5)
                        {
                            vertical = false;
                        }
                        else
                        {
                            vertical = true;
                            duration /= 2;
                        }
                        dashState = States.Dashing;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("DashXbox") && canDash && !player.GetComponent<PlayerHealth>().IsDead)
                    {
                        dashStart = Instantiate(dashStartParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(.15f, .6f);
                        fixedDirection = playerMovement.Direction;
                        duration = durationInput;
                        if (Mathf.Abs(Input.GetAxis("HorizontalXbox")) > .5)
                        {
                            vertical = false;
                        }
                        else
                        {
                            vertical = true;
                            duration /= 2;
                        }
                        dashState = States.Dashing;
                    }
                }
                break;

            case States.Dashing:
                if (duration >= 0 && !GetComponent<PlayerHealth>().IsDead)
                {
                    if(!vertical)
                    {
                        GameObject dashParticles = Instantiate(dashDurationParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                        ParticleSystem.ShapeModule shape = dashParticles.GetComponentInChildren<ParticleSystem>().shape;
                        shape.rotation = new Vector3(0, 90 * -fixedDirection, 0);
                        playerRb.velocity = new Vector2(fixedDirection * dashSpeed, 0);
                    }
                    else
                    {
                        GameObject dashParticles = Instantiate(dashDurationParticles, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                        ParticleSystem.ShapeModule shape = dashParticles.GetComponentInChildren<ParticleSystem>().shape;
                        shape.rotation = new Vector3(90, 90, 0);
                        playerRb.velocity = new Vector2(0, dashSpeed / 10);
                    }
                    playerRb.gravityScale = 0;
                    duration -= Time.deltaTime;

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
