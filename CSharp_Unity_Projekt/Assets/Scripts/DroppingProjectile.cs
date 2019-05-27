using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float timeToLive;

    public float Speed { get => speed; set => speed = value; }

    private void Update()
    {
        if (timeToLive <= 0)
            Destroy(this.gameObject);
        else
            timeToLive -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);


        if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("Projectile"))
            Destroy(this.gameObject);
    }
}
