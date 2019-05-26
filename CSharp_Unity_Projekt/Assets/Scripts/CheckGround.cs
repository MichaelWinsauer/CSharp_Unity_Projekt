using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameObject player;
    private float height;

    void Start()
    {
        player = transform.parent.gameObject;
        height = 50f;
    }
    
    public bool checkRaycastDistance()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("Ground")).distance == 0 && Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("Ground")).collider.tag.Equals("Ground"))
            return true;
        else
            return false;
    }
}
