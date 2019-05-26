using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedActive = 7;
    [SerializeField]
    private float moveSpeedPassive = 3;
    [SerializeField]
    private float viewArea = 5;
    [SerializeField]
    private int patrolDurationMinInput = 2;
    [SerializeField]
    private int patrolDurationMaxInput = 4;
    [SerializeField]
    private float jumpForce = 20;
    [SerializeField]
    private float jumpFrequencyInput;

    private bool isGrounded;
    private float jumpFrequency;
    private float patrolDuration;
    private bool isInView;
    private Rigidbody2D rb;
    private GameObject player;
    private int position;
    private int patrolDirection = 1;
    
    //Referenz auf den Spieler und auf den Rigidbody2D des Gegners
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    //Hier wird lediglich getestet, ob sich der Spieler in der Reichweite des Gegners befindet oder nicht. Wenn das der Fall ist greift der Gegner den Spieler an.
    //Wenn sich der Spieler außerdem über dem Gegner befindet springt dieser auch.
    //Andernfalls patroulliert der Gegner.
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < viewArea)
            isInView = true;
        else
            isInView = false;    

        if (isInView)
        {
            if (player.transform.position.x < transform.position.x)
            {
                position = -1;
                transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
            else
            {
                position = 1;
                transform.rotation = Quaternion.Euler(0f, 0f, 0);
            }
            if(Mathf.Abs(player.transform.position.x - transform.position.x) >= 1)
            {
                rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
                if (player.transform.position.y > transform.position.y && Mathf.Abs(player.transform.position.x - transform.position.x) >= 1 && Mathf.Abs(player.transform.position.x - transform.position.x) <= 3 && isGrounded)
                    jump();
            }
        }
        else
        {
            patrol();
        }
    }

    //Der Sprung des Gegners hat eine kurze Abklingzeit. Wenn diese abgelaufen ist kann er wieder Springen.
    private void jump()
    {
        jumpFrequency -= Time.deltaTime;
        if (jumpFrequency <= 0)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else
            jumpFrequency = jumpFrequencyInput;
    }

    //Der Gegner läuft für eine zufällige Zeit in eine Richtung. Wenn die Zeit abgelaufen ist, dreht er um und wiederholt den Schritt.
    private void patrol()
    {
        patrolDuration -= Time.deltaTime;
        if(patrolDuration <= 0)
        {   
            patrolDirection = -patrolDirection;
            if (patrolDirection == -1)
                transform.rotation = Quaternion.Euler(0f, 180, 0f);
            else
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            patrolDuration = new System.Random().Next(patrolDurationMinInput, patrolDurationMaxInput);
        }
        rb.velocity = new Vector2(patrolDirection * moveSpeedPassive, rb.velocity.y);
    }


    //Wenn der Gegner beim patroullieren eine Wand berührt dreht dieser sofort wieder um.
    //Außerdem wird hier getestet, ob der Gegner den Boden berührt.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isInView)
            patrolDuration = 0;
        else
            if (collision.collider.gameObject.tag.Equals("Ground"))
                isGrounded = true;
            else
                isGrounded = false;
    }
}
