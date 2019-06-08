using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int healthInput;
    [SerializeField]
    private float knockbackDurationInput;
    [SerializeField]
    private int knockbackForce;

    private int health;
    private float damageTimer = 1;
    private Rigidbody2D rb;
    private int knockbackDirection;
    private float knockbackDuration;


    public int Health { get => health; set => health = value; }
    public int KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    public float KnockbackDurationInput { get => knockbackDurationInput; set => knockbackDurationInput = value; }
    public float KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }

    //Komponentenreferenzen erstellt.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = healthInput;
    }

    private void Update()
    {
        KnockBack();
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
        //TODO: Instanciate Geld/Health/Punkte

        Destroy(this.gameObject);
    }

    private void KnockBack()
    {
        knockbackDuration -= Time.deltaTime;
        if(knockbackDuration >= 0)
        {
            GetComponent<EnemyMovement>().CanMove = false;
            rb.velocity = new Vector2(knockbackDirection * knockbackForce * 5, knockbackForce);
        }
        else
        {
            GetComponent<EnemyMovement>().CanMove = true;
        }
    }
}
