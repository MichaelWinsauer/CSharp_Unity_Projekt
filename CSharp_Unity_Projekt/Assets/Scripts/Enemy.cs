using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int healthInput;

    private int health;
    private float damageTimer = 1;
    private Rigidbody2D rb;
    public int Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = healthInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0)
        {
            damageTimer = 1;
            if (collision.gameObject.tag.Equals("Player"))
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        if ((health -= damage) <= 0)
            die();
    }

    private void die()
    {
        Destroy(this.gameObject);
    }
}
