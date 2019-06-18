using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
{
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<PlayerHealth>().CurrentHealth);
        else if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().TakeDamage(collision.gameObject.GetComponent<Enemy>().Health);
    }
}
