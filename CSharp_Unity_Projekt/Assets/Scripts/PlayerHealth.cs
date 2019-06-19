using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private GameObject deathParticles;
    [SerializeField]
    private float timerInput;
    [SerializeField]
    private GameObject rangedEnemy;
    [SerializeField]
    private GameObject meleeEnemy;
    [SerializeField]
    private float knockbackDurationInput;
    [SerializeField]
    private float knockbackForce;

    private int currentHealth;
    private bool isDamaged = false;
    private Movement movement;
    private bool isDead = false;
    private Vector3 startPosition;
    private Vector3 startScaling;
    private float timer;
    private GameObject[] enemies;
    private List<EnemySpawnPoint> enemySpawnPoints;
    private int knockbackDirection;
    private float knockbackDuration;
    private Rigidbody2D rb;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    public float KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }
    public float KnockbackDurationInput { get => knockbackDurationInput; set => knockbackDurationInput = value; }

    //Wird noch befor den ganzen Start Methoden aufgerufen. Hier legt man fixe Werte fest.
    void Awake()
    {
        movement = GetComponent<Movement>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemySpawnPoints = new List<EnemySpawnPoint>();
        startPosition = transform.position;
        startScaling = transform.localScale;
        timer = timerInput;

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy.GetComponent<EnemyMovement>().IsRanged)
                enemySpawnPoints.Add(new EnemySpawnPoint(rangedEnemy, enemy.transform.position));
            else
                enemySpawnPoints.Add(new EnemySpawnPoint(meleeEnemy, enemy.transform.position));
        }
    }

    private void Update()
    {
        if(isDead)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                spawn();
                timer = timerInput;
            }
        }

        KnockBack();
    }

    private void spawn()
    {
        transform.position = startPosition;
        transform.localScale = startScaling;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        isDead = false;
        currentHealth = maxHealth;
        respawnAllEnemies();
    }

    private void respawnAllEnemies()
    {
        foreach(GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (e != null)
                Destroy(e);
        }

        foreach(EnemySpawnPoint e in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(e.Enemy);
            enemy.transform.position = e.Position;
        }
    }

    //Funktion wird von außen aufgerufen, damit der Spieler schaden nehmen kann.
    public void TakeDamage(int damageAmount)
    {
        isDamaged = true;
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        if(currentHealth <= 0 && !isDead)
        {
            die();
        }
    }

    private void KnockBack()
    {
        knockbackDuration -= Time.deltaTime;
        if (knockbackDuration >= 0)
        {
            GetComponent<Movement>().CanMove = false;
            rb.velocity = new Vector2(knockbackDirection * knockbackForce * 7, knockbackForce);
        }
        else
        {
            GetComponent<Movement>().CanMove = true;
        }
    }

    //Zerstörung des Objekts
    private void die()
    {
        isDead = true;

        //Animation & Sound spielen
        transform.localScale = new Vector3(0,0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Instantiate(deathParticles, transform.position, Quaternion.identity);

    }
}
