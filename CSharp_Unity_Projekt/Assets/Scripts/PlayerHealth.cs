using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;
    private bool isDamaged = false;
    private Movement movement;
    private bool isDead = false;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<Movement>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        isDamaged = true;
        currentHealth -= damageAmount;

        if(currentHealth <= 0 && !isDead)
        {
            die();
        }
    }

    private void die()
    {
        isDead = true;
        //Animation & Sound spielen. Movement und Fähigkeiten disablen
        
    }
}
