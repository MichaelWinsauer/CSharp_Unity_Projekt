using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdgeCheck : MonoBehaviour
{

    private Transform edgeCheck;
    private RaycastHit2D hitBottom;
    private RaycastHit2D hitRight;
    private void Start()
    {
        edgeCheck = gameObject.transform.GetChild(0);
    }

    private void Update()
    {
        bottomCheck();
        rightCheck();
    }

    private void rightCheck()
    {
        hitRight = Physics2D.Raycast(transform.position, Vector2.right * GetComponentInParent<EnemyMovement>().Direction, .5f);
        if (hitRight.collider != null && hitRight.collider.CompareTag("Ground"))
        {
            gameObject.GetComponentInParent<EnemyMovement>().Flip();
        }
    }

    private void bottomCheck()
    {
        hitBottom = Physics2D.Raycast(edgeCheck.position, Vector2.down, .5f);
        if (hitBottom.collider == null || !hitBottom.collider.CompareTag("Ground"))
        {
            gameObject.GetComponentInParent<EnemyMovement>().Flip();
        }
    }
}
