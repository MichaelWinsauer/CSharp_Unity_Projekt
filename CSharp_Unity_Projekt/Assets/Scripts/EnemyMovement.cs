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

    private float patrolDuration;
    private bool isInView;
    private Rigidbody2D rb;
    private GameObject player;
    private int position;
    private int patrolDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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

            rb.velocity = new Vector2(position * moveSpeedActive, rb.velocity.y);
        }
        else
        {
            patrol();
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        patrolDuration = 0;
    }
}
