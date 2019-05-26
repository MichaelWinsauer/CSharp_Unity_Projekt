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

    //Referenzen der SpielerObjekten
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<Movement>();
    }

    //Prüfen des Cooldowns, des Inputs. Wenn das übereinstimmt wird die Dauer des Dashes gesetzt.
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

    //Wenn die Dauer > 0 ist wird der Dash ausgeführt. Die Funktion wird jeden Frame aufgerufen.
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
