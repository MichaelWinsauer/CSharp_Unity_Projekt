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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            player.GetComponent<Movement>().isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<Movement>().isGrounded = false;
    }
}
