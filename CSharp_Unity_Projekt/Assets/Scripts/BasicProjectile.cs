using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float timeToLive;
    [SerializeField]
    public float moveSpeed;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

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
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(2);
            Debug.Log(collision.gameObject.GetComponent<Enemy>().Health);
        }

        if(!collision.gameObject.tag.Equals("Player"))
            Destroy(this.gameObject);
    }
}
