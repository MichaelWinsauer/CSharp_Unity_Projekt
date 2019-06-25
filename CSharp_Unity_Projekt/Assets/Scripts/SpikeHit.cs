using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    [SerializeField]
    private bool isWater;
    [SerializeField]
    private GameObject waterParticle;


    private void OnCollisionEnter2D(Collision2D collision)
{
        if (isWater)
            Instantiate(waterParticle, new Vector3(collision.transform.position.x, collision.transform.position.y - 1, collision.transform.position.z), Quaternion.identity);

        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<PlayerHealth>().CurrentHealth);
        else if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().TakeDamage(collision.gameObject.GetComponent<Enemy>().Health);
        
    }
}
