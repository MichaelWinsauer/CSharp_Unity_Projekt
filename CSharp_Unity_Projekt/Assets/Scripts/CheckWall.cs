﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    private GameObject player;
    private Movement playerMovement;
    private float length;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement>();
        length = 5;
    }

    public bool checkWallRaycast()
    {
        //if (Physics2D.Raycast(transform.position, new Vector2(playerMovement.Direction, 0), length, LayerMask.NameToLayer("Wall")).collider != null)
        //{
        //    if ((Physics2D.Raycast(transform.position, new Vector2(playerMovement.Direction, 0), length, LayerMask.NameToLayer("Ground")).distance == 0 || Physics2D.Raycast(transform.position, new Vector2(playerMovement.Direction, 0), length, LayerMask.NameToLayer("Wall")).distance == 0) && (Physics2D.Raycast(transform.position, new Vector2(playerMovement.Direction, 0), length, LayerMask.NameToLayer("Ground")).collider.tag.Equals("Ground") || Physics2D.Raycast(transform.position, new Vector2(playerMovement.Direction, 0), length, LayerMask.NameToLayer("Wall")).collider.tag.Equals("Wall")))
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        //else
        //    return false;

        return false;
    }
}
