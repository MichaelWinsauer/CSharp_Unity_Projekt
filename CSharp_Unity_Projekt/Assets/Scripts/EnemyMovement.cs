using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float sightRadius;

    private GameObject target;
    private Rigidbody2D rb;
    private int position;
    private float timer = 1000;
    private int direction;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        System.Random r = new System.Random();
        direction = r.Next(-1, 2);
    }

    private void Update()
    {
        if (target.transform.position.x > transform.position.x)
            position = 1;
        else
            position = -1;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(target.transform.position.x - transform.position.x) < sightRadius)
            rb.velocity = new Vector2(position * moveSpeed, rb.velocity.y);
        else
            idle();
    }

    private void idle()
    {

        rb.velocity = new Vector2(direction * moveSpeed / 2, rb.velocity.y);

    }
}
