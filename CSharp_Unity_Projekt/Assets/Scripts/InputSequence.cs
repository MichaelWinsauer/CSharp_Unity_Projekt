using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSequence : MonoBehaviour
{
    
    private void Start()
    {
        new Sequence("Basic", new KeyCode[] { KeyCode.J });
        new Sequence("Sequence 2", new KeyCode[] { KeyCode.U, KeyCode.I, KeyCode.O });
    }

    // Update is called once per frame
    void Update()
    {
        checkInputs();
    }

    public void checkInputs()
    {
        if (Input.anyKeyDown)
        {
            foreach(Sequence s in Sequence.Sequences)
            {
                if (Input.GetKeyDown(s.Keys[s.Index]))
                {
                    s.Index++;

                    if (s.Index == s.Keys.Length)
                    {
                        s.Index = 0;

                        GetComponent<CastAbility>().checkAbility(s.Name);
                    }
                }
                else if (!Input.GetKeyDown(s.Keys[s.Index]))
                    s.Index = 0;
            }
        }
    }
}
