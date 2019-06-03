using System;
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
    [SerializeField]
    private GameObject rayLower;
    [SerializeField]
    private GameObject rayMiddle;
    [SerializeField]
    private GameObject rayUpper;

    private Movement player;

    //Referenz auf Spielerobjekt
    private void Start()
    {
        player = GetComponent<Movement>();
    }


    //Hier wird getestet, ob einer der Raycasts den Boden berührt. Bei mindestens einem der 3 ist der Spieler auf dem Boden.
    void FixedUpdate()
    {
        if (groundCheck())
            player.IsGrounded = true;
        else
            player.IsGrounded = false;

        if (wallCheck())
            player.TouchingWall = true;
        else
            player.TouchingWall = false;


    }

    private bool groundCheck()
    {
        if (rayLeft.GetComponent<CheckGround>().checkRaycastDistance() || rayCenter.GetComponent<CheckGround>().checkRaycastDistance() || rayRight.GetComponent<CheckGround>().checkRaycastDistance())
            return true;
        else
            return false;
    }

    private bool wallCheck()
    {
        if (rayLower.GetComponent<CheckWall>().checkWallRaycast() || rayMiddle.GetComponent<CheckWall>().checkWallRaycast() || rayUpper.GetComponent<CheckWall>().checkWallRaycast())
        {
            Debug.Log("wallCheck()");

            if (!groundCheck())
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
