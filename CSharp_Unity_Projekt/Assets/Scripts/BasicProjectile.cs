using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float timeToLive;
    public float moveSpeed;
    private Transform player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        if (player.rotation.y == 0)
            rb.velocity = Vector2.right * moveSpeed;
        else
            rb.velocity = Vector2.left * moveSpeed;
    }

    private void Update()
    {
        if (timeToLive <= 0)
            Destroy(this.gameObject);
        else
            timeToLive -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) ;
//            collision.gameObject.GetComponent<Enemy>().TakeDamage(2);

        Destroy(this.gameObject);
    }
}
