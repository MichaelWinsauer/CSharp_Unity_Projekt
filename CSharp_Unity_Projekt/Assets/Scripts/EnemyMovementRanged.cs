using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRanged : MonoBehaviour
{
    [SerializeField]
    private float viewRange;
    [SerializeField]
    private float moveSpeed;

    //Ranged specific
    [SerializeField]
    private float backDistance;


    private GameObject player;
    private Rigidbody2D rb;
    private EnemyShootProjectile shootProjectile;
    private bool playerInView;
    private bool canMove;
    private int direction;

    public bool CanMove { get => canMove; set => canMove = value; }
    public int Direction { get => direction; set => direction = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        shootProjectile = GetComponent<EnemyShootProjectile>();
    }

    
    void Update()
    {
        playerInView = inSight();

        if(canMove)
        {
            if (playerInView)
            {
                if (playerToEnemy() == 1)
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                else
                    transform.rotation = Quaternion.Euler(0f, -180f, 0f);

                shootProjectile.CanShoot = true;

                if (Vector2.Distance(player.transform.position, transform.position) < backDistance)
                {
                    if (checkForGround(-playerToEnemy()) && !checkForWall(-playerToEnemy()))
                        rb.velocity = new Vector2(moveSpeed * -playerToEnemy(), rb.velocity.y);
                    else
                        rb.velocity = new Vector2(0, rb.velocity.y);

                }
            }
            else
            {
                if (checkForWall(direction))
                    Flip();

                if(!checkForGround(direction))
                    Flip();

                rb.velocity = new Vector2(moveSpeed / 2 * direction, rb.velocity.y);
                shootProjectile.CanShoot = false;
            }
        }

        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            GetComponentInChildren<Animator>().SetBool("isRunning", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isRunning", false);
        }
    }

    private bool checkForGround(int rayPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * rayPosition, transform.position.y - 1), Vector2.down, 2f);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
            return true;
        else
            return false;
    }

    private bool checkForWall(int rayPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + .5f * rayPosition, transform.position.y), Vector2.right * rayPosition, .5f);
        if (hit.collider != null)
            return true;
        else
            return false;
    }

    private bool inSight()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y), new Vector2(player.transform.position.x - transform.position.x - 1 * playerToEnemy(), player.transform.position.y - transform.position.y), viewRange);
        if (playerHit.collider != null && (playerHit.collider.CompareTag("Player") || playerHit.collider.CompareTag("MeleeTrigger")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int playerToEnemy()
    {
        if (player.transform.position.x > transform.position.x)
            return 1;
        else
            return -1;
    }

    public int GetDirection()
    {
        if (transform.position.y == 0)
            direction = -1;
        else
            direction = 1;

        return direction;
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
