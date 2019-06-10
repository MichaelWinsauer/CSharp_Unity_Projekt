using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float timerInput;

    private CircleCollider2D hitbox;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GameObject.FindGameObjectWithTag("MeleeTrigger").GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            hitbox.enabled = false;
        }
        else
        {
            if(Input.GetButtonDown("Melee"))
            {
                hitbox.enabled = true;
                timer = timerInput;
            }
        }
    }
}
