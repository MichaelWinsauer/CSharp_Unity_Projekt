using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject waterParticle;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(waterParticle, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            collision.gameObject.GetComponent<PlayerHealth>().WaterDeath();
        }
    }
}

