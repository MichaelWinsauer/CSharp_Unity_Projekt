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

    //Komponentenreferenzen erstellt.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = healthInput;
    }

    //Wenn der Gegner den Spieler berührt soll dieser jede Sekunde dem Spieler schaden Machen.
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


    //Funktion, die von außen aufgerufen wird, wenn der Gegner schaden nehmen soll.
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
