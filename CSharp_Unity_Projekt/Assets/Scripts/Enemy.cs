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
    [SerializeField]
    private bool isRanged;

    private int health;
    private float damageTimer = 1;
    private Rigidbody2D rb;
    private int knockbackDirection;
    private float knockbackDuration;
    private int direction;
    private float knockbackTimer;


    public int Health { get => health; set => health = value; }
    public int KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    public float KnockbackDurationInput { get => knockbackDurationInput; set => knockbackDurationInput = value; }
    public float KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }
    public int KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
    public bool IsRanged { get => isRanged; set => isRanged = value; }

    //Komponentenreferenzen erstellt.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = healthInput;
    }

    private void Update()
    {
        if (knockbackTimer > 0)
            knockbackTimer -= Time.deltaTime;
        else
        {
            if(isRanged)
                GetComponent<EnemyMovementRanged>().CanMove = true;
            else
                GetComponent<EnemyMovement>().CanMove = true;
        }
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

                collision.gameObject.GetComponent<PlayerHealth>().Knockback(9, 9, .4f, direction);
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
        bodySplat.GetComponent<AudioSource>().volume = 0.3f;
        bodySplat.GetComponent<AudioSource>().Play();
    }

    public void Knockback(float amountX, float amountY, float duration)
    {
        if(isRanged)
            GetComponent<EnemyMovementRanged>().CanMove = false;
        else
            GetComponent<EnemyMovement>().CanMove = false;
        knockbackTimer = duration;
        if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            rb.AddForce(new Vector2(-amountX * 100, amountY * 50));
        else
            rb.AddForce(new Vector2(amountX * 100, amountY * 50));
    }
}
