using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float timeToLiveInput;
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject travelParticles;
    [SerializeField]
    private GameObject explosionParticles;

    private float timeToLive;
    private int direction;
    private GameObject particle;
    private float particleTimer;
    private float rotation;
    private bool isReflected;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public bool IsReflected { get => isReflected; set => isReflected = value; }
    public float TimeToLive { get => timeToLive; set => timeToLive = value; }
    public float TimeToLiveInput { get => timeToLiveInput; set => timeToLiveInput = value; }

    private void Start()
    {
        particleTimer = .01f;
        isReflected = false;
        timeToLive = timeToLiveInput;
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

        if(isReflected)
        {
            if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("BasicProjectile") && !collision.gameObject.CompareTag("EnemyProjectile") && !collision.gameObject.CompareTag("MeleeTrigger"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage / 2);
                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("BasicProjectile") && !collision.gameObject.CompareTag("EnemyProjectile") && !collision.gameObject.CompareTag("MeleeTrigger"))
            {
                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
