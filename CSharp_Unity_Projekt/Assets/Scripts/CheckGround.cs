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
    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 15f, LayerMask.NameToLayer("Ground")).distance == 0)
            player.GetComponent<Movement>().isGrounded = true;
        else
            player.GetComponent<Movement>().isGrounded = false;
    }
}
