using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(5);
        }
    }
}
