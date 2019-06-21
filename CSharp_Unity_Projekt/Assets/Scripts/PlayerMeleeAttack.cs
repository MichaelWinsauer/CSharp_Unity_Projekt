using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float timerInput;

    private CapsuleCollider2D hitbox;
    private float timer;
    private Animator anim;

    public float TimerInput { get => timerInput; set => timerInput = value; }
    public CapsuleCollider2D Hitbox { get => hitbox; set => hitbox = value; }
    public float Timer { get => timer; set => timer = value; }

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GameObject.FindGameObjectWithTag("MeleeTrigger").GetComponent<CapsuleCollider2D>();
        anim = GameObject.FindGameObjectWithTag("MeleeTrigger").GetComponent<Animator>();
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
                anim.SetTrigger("hit");
                hitbox.enabled = true;
                timer = timerInput;

            }
        }
    }
}
