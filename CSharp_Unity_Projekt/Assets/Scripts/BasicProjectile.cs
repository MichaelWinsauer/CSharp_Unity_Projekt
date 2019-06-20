using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float timeToLive;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject travelParticles;
    [SerializeField]
    private GameObject explosionParticles;

    private GameObject player;
    private int direction;
    private float rotation;
    private float particleTimer;
    private GameObject particle;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Rotation { get => rotation; set => rotation = value; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particleTimer = .01f;
    }

    //Lebenszeit des Projektils wird definiert
    private void Update()
    {
        if (timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(particleTimer <= 0)
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


    //Wird ausgeführt, sobald das Projektil etwas berührt. Abhängig vom berührten Objekt soll sich das Projektil dementsprechend verhalten.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(35);

            if (collision.gameObject.transform.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;

            collision.gameObject.GetComponent<Enemy>().KnockbackDirection = direction;
            collision.gameObject.GetComponent<Enemy>().KnockbackDuration = collision.gameObject.GetComponent<Enemy>().KnockbackDurationInput;
        }

        if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("MeleeTrigger") && !collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
            
    }
}
