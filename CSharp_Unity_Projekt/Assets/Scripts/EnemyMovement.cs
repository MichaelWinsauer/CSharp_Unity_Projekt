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
    private int playerToEnemy;
    private RaycastHit2D groundCheck;
    private RaycastHit2D playerHit;
    private bool groundExits;

    public bool IsRanged { get => isRanged; set => isRanged = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public int Direction { get => direction; set => direction = value; }


    //Referenz auf den Spieler und auf den Rigidbody2D des Gegners
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        jumpFrequency = jumpFrequencyInput;
    }

    //Hier wird lediglich getestet, ob sich der Spieler in der Reichweite des Gegners befindet oder nicht. Wenn das der Fall ist greift der Gegner den Spieler an.
    //Wenn sich der Spieler außerdem über dem Gegner befindet springt dieser auch.
    //Andernfalls patroulliert der Gegner.
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            playerToEnemy = -1;

            if (!isInView)
                Quaternion.Euler(0f, -180f, 0f);
        }
        else
            playerToEnemy = 1;

        if (canMove)
        {
            isInView = checkPlayerPosition();

            if (Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy, transform.position.y - 1), Vector2.down, 2f).collider != null)
            {
                if(Physics2D.Raycast(new Vector2(transform.position.x + 1 * direction, transform.position.y - 1), Vector2.down, 2f).collider.CompareTag("Ground"))
                    groundExits = true;
            }
            else
                groundExits = false;

            if (isRanged)
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
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) >= .5f)
                    {
                        if(groundExits)
                        {
                            rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
                            
                        }
                        else
                        {
                            rb.velocity = new Vector2(0, rb.velocity.y);
                        }
                    }
                }
                else
                {
                    if(groundExits)
                    {
                        if (Math.Abs(player.transform.position.x - transform.position.x) > stopDistance)
                        {
                            rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
                        }

                        if (Mathf.Abs(player.transform.position.x - transform.position.x) < backDistance)
                        {
                            rb.velocity = new Vector2(-position * moveSpeedActive, rb.velocity.y);
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
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
        if(!isRanged)
            jump();
    }

    private bool checkPlayerPosition()
    {
        playerHit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy, transform.position.y), new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewArea);
        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void shoot()
    {
        Vector3 spawnPoint = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform.position;
        Vector2 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if(shootTimer <= 0)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            GetComponentInChildren<Animator>().SetTrigger("shoot");
            GameObject projectile = Instantiate(enemyProjectile, spawnPoint, Quaternion.Euler(0f, 0f, rotationZ));
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
        if(jumpFrequency > 0)
        {
            jumpFrequency -= Time.deltaTime;
        }
        else
        {
            if (player.transform.position.y + 2 > transform.position.y && Mathf.Abs(player.transform.position.x - transform.position.x) >= 1
                && Mathf.Abs(player.transform.position.x - transform.position.x) <= 3)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpFrequency = jumpFrequencyInput;
            }
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
