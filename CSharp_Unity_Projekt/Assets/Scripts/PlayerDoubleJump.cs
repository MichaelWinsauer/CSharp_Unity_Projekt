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
    //Wird für die Movementsprerrung benutzt. 
    private float blockDuration;
    private float amountX;
    
    //Spielerreferenzen erzeugt
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    //Wenn die Abklingzeit der Fähigkeit abgelaufen ist wird die Spielereingabe getestet. Stimmt diese überein, wird die Dauer des Sprungs gesetzt.
    //Außerdem kann das nur in der Luft passieren. Also wenn der Spieler nicht den Boden berührt.
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
            if(!playerMovement.IsGrounded)
                if(Input.GetKeyDown(KeyCode.W))
                {
                    cooldown = cooldownInput;
                    duration = durationInput;
                    blockDuration = durationInput * 1.5f;
                    amountX = playerRb.velocity.x / 2;
                }

        doubleJump();
    }

    //Wenn die Dauer des Sprungs größer als 0 ist bewegt sich der Spieler erneut nach oben in Form eines zweiten Sprungs.
    private void doubleJump()
    {
        
        if(duration >= 0)
        {
            duration -= Time.deltaTime;

            if(amountX > 0)
            {
                amountX -= Time.deltaTime;
            }

            playerRb.velocity = new Vector2(amountX, jumpForce);
        }

        //Die Movementsprerre ist etwas länger als die Dauer des Sprunges, da der Sprungvektor nicht direkt wieder auf 0 geht. 
        //Das dient nur dazu, damit der Spieler nicht abrupt abbremst, wenn er den 2. Sprung tätigt.
        //Allgemein soll der Spieler aber sich nicht all zu sehr auf der X Achse wärend des 2. Sprunges bewegen.
        if (blockDuration >= 0)
        {
            playerMovement.CanMove = false;
            blockDuration -= Time.deltaTime;
        }
        else
        {
            playerMovement.CanMove = true;
        }
    }
}
