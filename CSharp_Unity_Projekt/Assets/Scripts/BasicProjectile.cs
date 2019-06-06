using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float timeToLive;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    private ParticleSystem emit;

    private GameObject player;
    private int direction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Lebenszeit des Projektils wird definiert
    private void Update()
    {
        if (timeToLive <= 0)
            Destroy(this.gameObject);
        else
            timeToLive -= Time.deltaTime;
    }


    //Wird ausgeführt, sobald das Projektil etwas berührt. Abhängig vom berührten Objekt soll sich das Projektil dementsprechend verhalten.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);

            if (collision.gameObject.transform.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;

            collision.gameObject.GetComponent<Enemy>().KnockbackDirection = direction;
            collision.gameObject.GetComponent<Enemy>().KnockbackDuration = collision.gameObject.GetComponent<Enemy>().KnockbackDurationInput;
            Debug.Log(collision.gameObject.GetComponent<Enemy>().Health);
        }

        if(!collision.gameObject.tag.Equals("Player"))
        {
            //emit.transform.parent = null;

            Destroy(this.gameObject);
        }
            
    }
}
