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
    [SerializeField]
    private GameObject deathParticles;
    [SerializeField]
    private GameObject healthObject;
    [SerializeField]
    private GameObject bodyDeathSound;

    private int health;
    private float damageTimer = 1;
    private Rigidbody2D rb;
    private int knockbackDirection;
    private float knockbackDuration;
    private int direction;


    public int Health { get => health; set => health = value; }
    public int KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    public float KnockbackDurationInput { get => knockbackDurationInput; set => knockbackDurationInput = value; }
    public float KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }
    public int KnockbackForce { get => knockbackForce; set => knockbackForce = value; }

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (damageTimer >= 0)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(30);

                if (collision.gameObject.transform.position.x > transform.position.x)
                    direction = 1;
                else
                    direction = -1;

                collision.gameObject.GetComponent<PlayerHealth>().KnockbackDirection = direction;
                collision.gameObject.GetComponent<PlayerHealth>().KnockbackDuration = collision.gameObject.GetComponent<PlayerHealth>().KnockbackDurationInput;
                damageTimer = 1f;
            }
        }
        else
            damageTimer -= Time.deltaTime;
    }


    //Funktion, die von außen aufgerufen wird, wenn der Gegner schaden nehmen soll.
    public void TakeDamage(int damage)
    {
        if ((health -= damage) <= 0)
            die();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(.1f, .2f);
        
    }

    private void die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        if(UnityEngine.Random.Range(0, 4) == 1)
        {
            Instantiate(healthObject, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
        GameObject bodySplat = Instantiate(bodyDeathSound, transform.position, Quaternion.identity);
        bodySplat.GetComponent<AudioSource>().clip = FindObjectOfType<AudioManager>().GetSource("BodySplat").clip;
        bodySplat.GetComponent<AudioSource>().volume = 0.5f;
        bodySplat.GetComponent<AudioSource>().Play();
    }

    private void KnockBack()
    {
        knockbackDuration -= Time.deltaTime;
        if(knockbackDuration >= 0)
        {
            GetComponent<EnemyMovement>().CanMove = false;
            rb.velocity = new Vector2(knockbackDirection * knockbackForce * 3, knockbackForce);
        }
        else
        {
            GetComponent<EnemyMovement>().CanMove = true;
        }
    }
}
