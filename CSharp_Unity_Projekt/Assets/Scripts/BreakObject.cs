using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    private float breakTimerInput;
    [SerializeField]
    private GameObject breakFallingParticles;
    [SerializeField]
    private GameObject breakParticles;

    private float breakTimer;
    private bool isBreaking;


    private void Start()
    {
        breakTimer = breakTimerInput;
    }

    void Update()
    {
        if(isBreaking)
        {
            if (breakTimer >= 0)
            {
                breakTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(breakParticles, transform);
                Instantiate(breakFallingParticles, transform);
                //Play Sound;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            isBreaking = true;
    }
}
