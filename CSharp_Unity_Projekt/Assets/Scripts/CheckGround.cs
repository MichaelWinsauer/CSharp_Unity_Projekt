using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider)
        {
            player.GetComponent<Movement>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.GetComponent<Movement>().isGrounded = false;
    }
}
