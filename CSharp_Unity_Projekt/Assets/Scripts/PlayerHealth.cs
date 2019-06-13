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
    public bool IsDead { get => isDead; set => isDead = value; }

    //Wird noch befor den ganzen Start Methoden aufgerufen. Hier legt man fixe Werte fest.
    void Awake()
    {
        movement = GetComponent<Movement>();
        currentHealth = maxHealth;
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

    //Zerstörung des Objekts
    private void die()
    {
        isDead = true;

        //Animation & Sound spielen. Movement und Fähigkeiten disablen

    }
}
