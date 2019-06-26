using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHelp : MonoBehaviour
{
    [SerializeField]
    private string controlName;
    [SerializeField]
    private bool isAxis;

    private Animator anim;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), 7f);
        if(hit.collider != null && (hit.collider.CompareTag("Player") || hit.collider.CompareTag("MeleeTrigger")))
        {
            if(!anim.GetBool("isDone"))
            {
                anim.SetTrigger("appear");
                if (isAxis)
                {
                    if (Input.GetAxis(controlName) != 0)
                    {
                        anim.SetTrigger("disappear");
                        anim.SetBool("isDone", true);
                    }
                }
                else
                {
                    if (Input.GetButtonDown(controlName))
                    {
                        anim.SetTrigger("disappear");
                        anim.SetBool("isDone", true);
                        
                    }
                }
            }
        }
        else
        {
            anim.SetTrigger("disappear");
        }
    }
}
