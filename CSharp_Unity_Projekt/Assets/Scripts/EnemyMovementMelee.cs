using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMelee : MonoBehaviour
{
    [SerializeField]
    private float viewRange;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    private GameObject player;
    private Rigidbody2D rb;
    private bool playerInRange;
    private int direction;
    private bool canMove;
    private float timer;
    private bool moveToPlayer;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }


    void Update()
    {
        playerInRange = inSight();

        if (canMove)
        {
            if(playerInRange)
            {
                if (playerToEnemy() == 1)
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                else
                    transform.rotation = Quaternion.Euler(0f, -180f, 0f);

                move();
                if (timer > 0)
                {
                    timer -= Time.deltaTime;

                    if (moveToPlayer)
                    {
                        move();
                    }
                    else
                        jump();
                }
                else
                {
                    if (Random.Range(0, 4) == 0)
                    {
                        if (Vector2.Distance(player.transform.position, transform.position) < 4)
                            moveToPlayer = false;
                        else
                            moveToPlayer = true;
                    }
                    else
                        moveToPlayer = true;
                    timer = 1f;
                }
            }
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

    private void move()
    {
        if (checkForGround(playerToEnemy()) && !checkForWall(playerToEnemy()))
            rb.velocity = new Vector2(moveSpeed * playerToEnemy(), rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void jump()
    {
        Debug.DrawRay(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y - 1), Vector2.down, Color.blue, 2f);
        if (checkForGround(playerToEnemy()) && !checkForWall(playerToEnemy()))
            rb.velocity = new Vector2(moveSpeed * playerToEnemy(), jumpForce);
    }
}
