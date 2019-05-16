using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllRaycasts : MonoBehaviour
{

    public GameObject rayLeft;
    public GameObject rayRight;
    public GameObject rayCenter;
    private Movement player;

    private void Start()
    {
        player = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        if (rayLeft.GetComponent<CheckGround>().checkRaycastDistance() || rayCenter.GetComponent<CheckGround>().checkRaycastDistance() || rayRight.GetComponent<CheckGround>().checkRaycastDistance())
            player.isGrounded = true;
        else
            player.isGrounded = false;

    }
}
