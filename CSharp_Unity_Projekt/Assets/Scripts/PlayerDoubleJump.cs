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
                }

        doubleJump();
    }

    //Wenn die Dauer des Sprungs größer als 0 ist bewegt sich der Spieler erneut nach oben in Form eines zweiten Sprungs.
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
