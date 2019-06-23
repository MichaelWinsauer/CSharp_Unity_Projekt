using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedActive = 7;
    [SerializeField]
    private float moveSpeedPassive = 3;
    [SerializeField]
    private float viewArea = 5;
    [SerializeField]
    private float jumpForce = 20;
    [SerializeField]
    private float jumpFrequencyInput;

    //Ranged properties
    [SerializeField]
    private bool isRanged;
    [SerializeField]
    private float stopDistance;
    [SerializeField]
    private float backDistance;
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private float shootTimerInput;
    

    private bool isGrounded;
    private float jumpFrequency;
    private float patrolDuration;
    private bool isInView;
    private Rigidbody2D rb;
    private GameObject player;
    private int position;
    private int patrolDirection = 1;
    private bool canMove = true;
    private float shootTimer;
    private float projectileSpeed;
    private int direction;

    public bool IsRanged { get => isRanged; set => isRanged = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public int Direction { get => direction; set => direction = value; }


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
        if(canMove)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < viewArea)
                isInView = true;
            else
                isInView = false;

            if(isRanged)
            {
                if(rb.velocity.x > 0)
                {
                    GetComponentInChildren<Animator>().SetBool("isRunning", true);
                }
                else
                {
                    GetComponentInChildren<Animator>().SetBool("isRunning", false);
                }

            }

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

                if (!isRanged)
                {
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) >= 1)
                    {
                        rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
                        jump();
                    }
                }
                else
                {
                    if (Math.Abs(player.transform.position.x - transform.position.x) > stopDistance)
                    {
                        rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
                    }

                    if (Mathf.Abs(player.transform.position.x - transform.position.x) < backDistance)
                    {
                        rb.velocity = new Vector2(-position * moveSpeedActive, rb.velocity.y);
                    }

                    if(Math.Abs(player.transform.position.x - transform.position.x) == stopDistance)
                    {
                        transform.position = this.transform.position;
                    }

                    if(Mathf.Abs(player.transform.position.x - transform.position.x) <= stopDistance)
                    {
                        shoot();
                    }
                }
            }
            else
            {
                patrol();
            }
        }
    }

    private void shoot()
    {
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("EnemyProjectileSpawnpoint").transform.position;
        Vector2 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if(shootTimer <= 0)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            GetComponentInChildren<Animator>().SetTrigger("shoot");
            GameObject projectile = Instantiate(enemyProjectile, transform.position, Quaternion.Euler(0f, 0f, rotationZ));
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<EnemyProjectile>().MoveSpeed * Random.Range(.5f, 1.5f);
            projectile.GetComponent<EnemyProjectile>().Rotation = rotationZ;
            shootTimer = shootTimerInput * Random.Range(.5f, 1.5f);

        }
        else
        {
            shootTimer -= Time.deltaTime;
        }

    }

    //Der Sprung des Gegners hat eine kurze Abklingzeit. Wenn diese abgelaufen ist kann er wieder Springen.
    private void jump()
    {
        if (jumpFrequency <= 0)
        {
            if (player.transform.position.y + 2 > transform.position.y && Mathf.Abs(player.transform.position.x - transform.position.x) >= 1 
                && Mathf.Abs(player.transform.position.x - transform.position.x) <= 3 && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpFrequency = jumpFrequencyInput;
            }
        }
        else
        {
            jumpFrequency -= Time.deltaTime;        
        }
    }

    private void patrol()
    {
        rb.velocity = new Vector2(patrolDirection * moveSpeedPassive * direction, rb.velocity.y);
    }

    public void Flip()
    {
        if (transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0f, -180, 0f);
            direction = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
            direction = 1;
        }
    }
}
