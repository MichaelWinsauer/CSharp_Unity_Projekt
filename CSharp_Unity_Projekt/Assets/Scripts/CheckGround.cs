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
    
    public bool checkRaycastDistance()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 15f, LayerMask.NameToLayer("Ground")).distance == 0)
            return true;
        else
            return false;
    }
}
