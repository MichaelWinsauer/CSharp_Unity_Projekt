using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{

    [SerializeField]
    private float distance;
    [SerializeField]
    private LayerMask boxMask;

    private GameObject box;
    private GameObject[] boxes;
    private int direction;

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<PlayerHealth>().IsDead)
        {
            direction = GetComponent<Movement>().Direction;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(direction, 0), distance, boxMask);

            if (hit.collider != null && Input.GetButton("PullPush"))
            {
                box = hit.collider.gameObject;
                box.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
                box.GetComponent<HingeJoint2D>().enabled = true;
                GetComponent<Movement>().CanFlip = false;
                GetComponent<Movement>().CanJump = false;
                box.GetComponent<Rigidbody2D>().mass = 10;

                GetComponent<Animator>().SetBool("isGrabbing", true);
                GetComponent<Animator>().SetFloat("moveDirection", Input.GetAxis("Horizontal") * GetComponent<Movement>().Direction);
            }
            else
            {
                boxes = GameObject.FindGameObjectsWithTag("PullObject");

                foreach (GameObject b in boxes)
                {
                    b.GetComponent<HingeJoint2D>().enabled = false;
                    b.GetComponent<Rigidbody2D>().mass = 30;

                    GetComponent<Animator>().SetBool("isGrabbing", false);
                    GetComponent<Animator>().SetFloat("moveDirection", Input.GetAxis("Horizontal") * GetComponent<Movement>().Direction);
                }
            }

            if(hit.collider != null && Input.GetButtonUp("PullPush"))
            {
                GetComponent<Movement>().CanFlip = true;
                GetComponent<Movement>().CanJump = true;
            }
        }
    }
}
