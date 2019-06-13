using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float timeToLive;
    [SerializeField]
    private int damage;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive >= 0)
            timeToLive -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("BasicProjectile") && !collision.gameObject.CompareTag("MeleeTrigger"))
        {
            Destroy(this.gameObject);
        }
    }
}
