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
    private bool playerInRange;
    private bool canMove;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        playerInRange = inSight();

        if (playerInRange)
        {

        }
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
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
