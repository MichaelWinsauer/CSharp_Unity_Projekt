using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDistance : MonoBehaviour
{

    private GameObject closestEnemy;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BattleTheme");
    }

    void FixedUpdate()
    {
        getClosestEnemy();

        //Debug.Log(enemyInSight());

        if (enemyInSight())
        {
            //Dein Code hier und so :D
            if (FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume < 0.1f)
            {
                FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume += 0.001f;
            }
        }
        else
        {
            //oder halt hier und so :D
            if (FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume > 0f)
            {
                Invoke("stopBattleTheme", 3);
            }
        }
    }

    private void stopBattleTheme()
    {
        FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume -= 0.001f;
    }

    private void getClosestEnemy()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(closestEnemy != null)
            {
                if (Vector2.Distance(g.transform.position, transform.position) < Vector2.Distance(closestEnemy.transform.position, transform.position))
                    closestEnemy = g;
            }
            else
            {
                closestEnemy = g;
            }
        }
    }

    private int playerToEnemy()
    {
        if (transform.position.x < closestEnemy.transform.position.x)
            return 1;
        else
            return -1;
    }

    private bool enemyInSight()
    {
        Debug.DrawLine(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y), closestEnemy.transform.position);
        RaycastHit2D playerHit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y), new Vector2(closestEnemy.transform.position.x - transform.position.x, closestEnemy.transform.position.y - transform.position.y), 20f);
        if (playerHit.collider != null && playerHit.collider.CompareTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
