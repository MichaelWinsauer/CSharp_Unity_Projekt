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
    }

    
    void Update()
    {
        playerInView = inSight();

        if(canMove)
        {
            if (playerInView)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > backDistance)
                {
                    shootProjectile.CanShoot = true;
                }
                else
                {
                    shootProjectile.CanShoot = false;
                    if (checkForGround(-playerToEnemy()))
                        rb.velocity = new Vector2(moveSpeed * -playerToEnemy(), rb.velocity.y);
                    else
                        rb.velocity = new Vector2(0, rb.velocity.y);

                }
            }
            else
            {
                if (checkForWall())
                    Flip();

                if(checkForGround(playerToEnemy()))
                    Flip();
                rb.velocity = new Vector2(moveSpeed / 2, rb.velocity.y);
            }
        }
    }

    private bool checkForGround(int rayPosition)
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x + 1 * rayPosition, transform.position.y - 1), Vector2.down, 2f).collider != null)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x + 1 * direction, transform.position.y - 1), Vector2.down, 2f).collider.CompareTag("Ground"))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private bool checkForWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * GetComponentInParent<EnemyMovementRanged>().Direction, .5f);
        if (hit.collider != null)
            return true;
        else
            return false;
    }

    private bool inSight()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y), new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewRange);
        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
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

    public void Flip()
    {
        if (transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            direction = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            direction = 1;
        }
    }
}
