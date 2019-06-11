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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = GetComponent<Movement>().Direction;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(direction, 0), distance, boxMask);

        if (hit.collider != null && Input.GetButton("PullPush"))
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            box.GetComponent<FixedJoint2D>().enabled = true;
            GetComponent<Movement>().CanFlip = false;
            GetComponent<Movement>().CanJump = false;
            box.GetComponent<Rigidbody2D>().mass = 10;
        }
        else
        {
            boxes = GameObject.FindGameObjectsWithTag("PullObject");

            foreach(GameObject b in boxes)
            {
                b.GetComponent<FixedJoint2D>().enabled = false;
                GetComponent<Movement>().CanFlip = true;
                GetComponent<Movement>().CanJump = true;
                b.GetComponent<Rigidbody2D>().mass = 30;
            }
        }
    }
}
