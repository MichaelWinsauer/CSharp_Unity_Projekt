using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float timerInput;
    [SerializeField]
    private float activeInput;

    private CapsuleCollider2D hitbox;
    private float timer;
    private float active;
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
        }
        else
        {
            if(!GameData.options.UseXbox)
            {
                if (Input.GetButtonDown("Melee"))
                {
                    anim.SetTrigger("hit");
                    active = activeInput;
                    timer = timerInput;
                }
            }
            else
            {
                if (Input.GetButtonDown("MeleeXbox"))
                {
                    anim.SetTrigger("hit");
                    active = activeInput;
                    timer = timerInput;
                }

            }
        }

        if(active > 0)
        {
            hitbox.enabled = true;
            if(Input.GetButtonDown("Melee"))
            {
                anim.SetTrigger("hitAgain");
            }
            active -= Time.deltaTime;
        }
        else
        {
            hitbox.enabled = false;
        }
    }
}
