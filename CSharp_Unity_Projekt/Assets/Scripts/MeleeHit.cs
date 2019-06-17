using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    private int direction;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(20);

            if (collider.gameObject.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
                direction = 1;
            else
                direction = -1;


            collider.gameObject.GetComponent<Enemy>().KnockbackDirection = direction;
            collider.gameObject.GetComponent<Enemy>().KnockbackDuration = collider.gameObject.GetComponent<Enemy>().KnockbackDurationInput;
        }
    }
}
