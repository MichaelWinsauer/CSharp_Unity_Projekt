using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowDash : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerDash>().CanDash = true;
            Destroy(this.gameObject);
        }
    }
}
