using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdgeCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == null || !collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Turn");
            if (transform.rotation.y == 0)
            {
                transform.rotation = Quaternion.Euler(0f, -180, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
