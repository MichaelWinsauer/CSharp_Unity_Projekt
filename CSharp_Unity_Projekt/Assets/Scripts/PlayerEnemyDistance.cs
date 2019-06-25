using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDistance : MonoBehaviour
{

    private GameObject closestEnemy;

    void Update()
    {
        getClosestEnemy();

        if (enemyInSight())
        {
            //Dein Code hier und so :D
        }
        else
        {
            //oder halt hier und so :D
        }
    }

    private void getClosestEnemy()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (closestEnemy != null)
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
        else
        {
            closestEnemy = this.gameObject;
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
