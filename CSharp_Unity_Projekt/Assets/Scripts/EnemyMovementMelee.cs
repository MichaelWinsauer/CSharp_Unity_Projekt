using System;
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
    private Animator anim;
    private bool playerInRange;
    private int direction;
    private bool canMove;
    private bool canFlip;
    private float jumpDelay;
    private bool jumpPressed;

    public bool CanMove { get => canMove; set => canMove = value; }
    public bool CanFlip { get => canFlip; set => canFlip = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        canMove = true;
        canFlip = true;
        jumpPressed = false;
    }

    void Update()
    {
        playerInRange = inSight();

        if (canMove)
        {
            if(playerInRange)
            {
                if(canFlip)
                {
                    if (playerToEnemy() == 1)
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    else
                        transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                }

                move();

                if(Input.GetButtonDown("Jump") && !jumpPressed)
                {
                    jumpDelay = UnityEngine.Random.Range(.1f, .3f);
                    jumpPressed = true;
                    anim.SetTrigger("jump");
                }

                if (jumpPressed && jumpDelay > 0)
                    jumpDelay -= Time.deltaTime;
                else if (jumpPressed)
                {
                    jump();
                    jumpPressed = false;
                }
            }
            else
            {
                if (checkForWall(direction))
                    Flip();

                if (!checkForGround(direction))
                    Flip();

                rb.velocity = new Vector2(moveSpeed / 3 * direction, rb.velocity.y);
            }
        }

        if(Mathf.Abs(rb.velocity.x) > 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

    }

    private bool checkGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, .2f);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
            return true;
        else
            return false;
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
        RaycastHit2D playerHit = Physics2D.Raycast(new Vector2(transform.position.x + .6f * playerToEnemy(), transform.position.y), new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewRange);
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
        if (checkForGround(playerToEnemy()))
            rb.velocity = new Vector2(moveSpeed * playerToEnemy(), rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void jump()
    {
        if (checkForGround(playerToEnemy()) && checkGrounded())
            rb.AddForce(new Vector2(moveSpeed * 1.5f * playerToEnemy(), jumpForce));
    }
}
