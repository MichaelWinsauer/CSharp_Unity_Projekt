using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllRaycasts : MonoBehaviour
{
    [SerializeField]
    public GameObject rayLeft;
    [SerializeField]
    public GameObject rayRight;
    [SerializeField]
    public GameObject rayCenter;
    private Movement player;

    private void Start()
    {
        player = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        if (rayLeft.GetComponent<CheckGround>().checkRaycastDistance() || rayCenter.GetComponent<CheckGround>().checkRaycastDistance() || rayRight.GetComponent<CheckGround>().checkRaycastDistance())
            player.IsGrounded = true;
        else
            player.IsGrounded = false;

    }
}
