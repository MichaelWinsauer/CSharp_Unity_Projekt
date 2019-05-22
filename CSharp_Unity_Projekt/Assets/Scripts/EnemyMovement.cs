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
    private float viewArea = 10;
    [SerializeField]
    private int patrolDurationMinInput = 3;
    [SerializeField]
    private int patrolDurationMaxInput = 7;

    private float patrolDuration;
    private bool isInView;
    private Rigidbody2D rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        patrolDuration = new System.Random().Next(patrolDurationMinInput, patrolDurationMaxInput);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x) < viewArea)
            isInView = true;
        else
            isInView = false;

        if (!isInView)
        {
            if (player.transform.position.x < transform.position.x)
            {
                rb.velocity *= 1;
                transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
            else
            {
                rb.velocity *= -1;
                transform.rotation = Quaternion.Euler(0f, 0f, 0);
            }
            rb.velocity = new Vector2(moveSpeedActive, rb.velocity.y);
        }
        else
        {
            patrol();
        }
    }

    private void patrol()
    {
        patrolDuration -= Time.deltaTime;
        if(patrolDuration > 0)
        {
            patrolDuration = new System.Random().Next(patrolDurationMinInput, patrolDurationMaxInput);
            rb.velocity *= -1;
        }
        rb.velocity = new Vector2(moveSpeedActive, rb.velocity.y);
    }
}
