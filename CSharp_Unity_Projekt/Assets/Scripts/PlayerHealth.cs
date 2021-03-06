﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
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
    private float knockbackTimer;
    private bool isWater = false;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    public float KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }
    public float KnockbackDurationInput { get => knockbackDurationInput; set => knockbackDurationInput = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsWater { get => isWater; set => isWater = value; }

    //Wird noch befor den ganzen Start Methoden aufgerufen. Hier legt man fixe Werte fest.
    void Awake()
    {
        movement = GetComponent<Movement>();
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
            if(enemy.GetComponent<Enemy>().IsRanged)
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
        GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>().fillAmount = (float)currentHealth / maxHealth;

        if (currentHealth >= 100)
            GameObject.FindGameObjectWithTag("HealthBar").transform.parent.parent.parent.GetChild(2).GetComponentInChildren<Text>().text = 100.ToString();
        else if (currentHealth >= 0)
            GameObject.FindGameObjectWithTag("HealthBar").transform.parent.parent.parent.GetChild(2).GetComponentInChildren<Text>().text = currentHealth.ToString();
        else
            GameObject.FindGameObjectWithTag("HealthBar").transform.parent.parent.parent.GetChild(2).GetComponentInChildren<Text>().text = 0.ToString();

        GameObject.FindGameObjectWithTag("HealthBar").transform.parent.parent.parent.GetChild(3).GetComponentInChildren<Text>().text = GameData.deathCount.ToString();

        if (knockbackTimer > 0)
            knockbackTimer -= Time.deltaTime;
        else
            GetComponent<Movement>().CanMove = true;
    }

    private void spawn()
    {
        transform.position = startPosition;
        transform.localScale = startScaling;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        isDead = false;
        GameData.player.Health = maxHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void respawnAllEnemies()
    {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (e != null)
                Destroy(e);
        }

        foreach (EnemySpawnPoint e in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(e.Enemy);
            enemy.transform.position = e.Position;
        }
    }

    //Funktion wird von außen aufgerufen, damit der Spieler schaden nehmen kann.
    public void TakeDamage(int damageAmount)
    {
        isDamaged = true;
        GameObject.FindGameObjectWithTag("PlayerHit").GetComponent<Animator>().SetTrigger("hit");
        currentHealth -= damageAmount;

        if(currentHealth > 0)
            FindObjectOfType<AudioManager>().Play("PlayerTakeDamage");

        if(currentHealth <= 0 && !isDead)
        {
            die(false);
        }
    }

    public void Knockback(float amountX, float amountY, float duration, int direction)
    {
        GetComponent<Movement>().CanMove = false;
        knockbackTimer = duration;

            rb.AddForce(new Vector2(amountX * direction * 100, amountY * 50));
    }

    public void WaterDeath()
    {
        GameObject.FindGameObjectWithTag("PlayerHit").GetComponent<Animator>().SetTrigger("hit");
        currentHealth = 0;

        if (currentHealth > 0)
            FindObjectOfType<AudioManager>().Play("PlayerTakeDamage");

        if (currentHealth <= 0 && !isDead)
        {
            die(true);
        }
    }

    //Zerstörung des Objekts
    private void die(bool isWater)
    {
        isDead = true;
        this.isWater = isWater;
        GameData.deathCount++;

        //Animation & Sound spielen
        
        if(!isWater)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.localScale = new Vector3(0, 0);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("BodySplat");
        }
        else if (isWater)
        {
            FindObjectOfType<AudioManager>().Play("WaterSplash");
        }
    }
}
