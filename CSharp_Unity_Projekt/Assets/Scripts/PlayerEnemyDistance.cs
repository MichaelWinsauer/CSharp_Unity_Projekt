using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDistance : MonoBehaviour
{

    private GameObject closestEnemy;

    void Update()
    {
        closestEnemy = getClosestEnemy();

        if(enemyInSight())
        {
            //Dein Code hier und so :D
        }
        else
        {

        }
    }

    private GameObject getClosestEnemy()
    {
        GameObject close = new GameObject();
        close.transform.position = new Vector3(999f, 999f, 999f);

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(close != null)
            {
                if (Vector2.Distance(g.transform.position, transform.position) < Vector2.Distance(close.transform.position, transform.position))
                    close = g;
            }
        }

        return close;
    }

    private int playerToEnemy()
    {
        if (transform.position.x > closestEnemy.transform.position.x)
            return 1;
        else
            return -1;
    }

    private bool enemyInSight()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * playerToEnemy(), transform.position.y), new Vector2(closestEnemy.transform.position.x - transform.position.x, closestEnemy.transform.position.y - transform.position.y), 20f);
        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
