using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeHitParticle;

    private int direction;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(35);

            if (collider.gameObject.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
                direction = 1;
            else
                direction = -1;

            collider.gameObject.GetComponent<Enemy>().Knockback(9, 9, .3f);
            Instantiate(meleeHitParticle, collider.gameObject.transform.position, Quaternion.identity);

            FindObjectOfType<AudioManager>().Play("MeleeHit");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("MeleeMiss");
        }

        if (collider.CompareTag("EnemyProjectile"))
        {
            if(!collider.GetComponent<EnemyProjectile>().IsReflected)
            {
                Instantiate(meleeHitParticle, collider.gameObject.transform.position, Quaternion.identity);
                GameObject enemyProjectile = collider.gameObject;
                enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemyProjectile.GetComponent<Rigidbody2D>().velocity.x, -enemyProjectile.GetComponent<Rigidbody2D>().velocity.y);
                enemyProjectile.transform.rotation = Quaternion.Euler(enemyProjectile.transform.rotation.x * -1, enemyProjectile.transform.rotation.y * -1, enemyProjectile.transform.rotation.z * -1);
                enemyProjectile.GetComponent<EnemyProjectile>().IsReflected = true;
                enemyProjectile.GetComponent<EnemyProjectile>().TimeToLive = enemyProjectile.GetComponent<EnemyProjectile>().TimeToLiveInput;
            }
        }
    }
}
