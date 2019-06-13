using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private LayerMask pullObject;

    private GameObject player;
    private float height;

    //Spielerreferenzen erstellen.
    void Start()
    {
        player = transform.parent.gameObject;
        height = 50f;
    }
    
    //Die Funktion gibt zurück, ob die Distanz zum Objekt unter einem 0 ist und ob es sich um den Boden handelt.
    //Die funktion wird jeden Frame von außen aufgerufen.
    //Das heißt sie wird pro Sekunde 60*3 also 180 mal aufgerufen, da es 3 Raycasts am Spieler gibt.
    public bool checkRaycastDistance()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, height, ground).distance == 0 || Physics2D.Raycast(transform.position, Vector2.down, height, pullObject).distance == 0)
        {
            //(Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("Ground")).distance == 0 
            //&& Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("Ground")).collider.CompareTag("Ground"))
            //|| (Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("PullObject")).distance == 0
            //&& Physics2D.Raycast(transform.position, Vector2.down, height, LayerMask.NameToLayer("PullObject")).collider.CompareTag("PullObject")))
            return true;
        }
        else
            return false;
    }
}
