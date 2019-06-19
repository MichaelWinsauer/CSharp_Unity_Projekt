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
    [SerializeField]
    private GameObject travelParticles;

    private int direction;
    private GameObject particle;
    private float particleTimer;
    private float rotation;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Rotation { get => rotation; set => rotation = value; }

    private void Start()
    {
        particleTimer = .01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (particleTimer <= 0)
            {
                particle = Instantiate(travelParticles, transform.position, Quaternion.identity);
                ParticleSystem.ShapeModule shape = particle.GetComponent<ParticleSystem>().shape;
                shape.rotation = new Vector3(rotation, -90, 0);
                particleTimer = .01f;
            }
            else
            {
                particleTimer -= Time.deltaTime;
            }
            timeToLive -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            if (collision.gameObject.transform.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;

            collision.gameObject.GetComponent<PlayerHealth>().KnockbackDirection = direction;
            collision.gameObject.GetComponent<PlayerHealth>().KnockbackDuration = collision.gameObject.GetComponent<PlayerHealth>().KnockbackDurationInput;
        }

        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("BasicProjectile") && !collision.gameObject.CompareTag("MeleeTrigger"))
        {
            Destroy(this.gameObject);
        }
    }
}
