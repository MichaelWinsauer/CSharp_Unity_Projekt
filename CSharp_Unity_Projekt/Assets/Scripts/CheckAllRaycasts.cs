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

    //Referenz auf Spielerobjekt
    private void Start()
    {
        player = GetComponent<Movement>();
    }


    //Hier wird getestet, ob einer der Raycasts den Boden berührt. Bei mindestens einem der 3 ist der Spieler auf dem Boden.
    void FixedUpdate()
    {
        if (rayLeft.GetComponent<CheckGround>().checkRaycastDistance() || rayCenter.GetComponent<CheckGround>().checkRaycastDistance() || rayRight.GetComponent<CheckGround>().checkRaycastDistance())
            player.IsGrounded = true;
        else
            player.IsGrounded = false;

    }
}
