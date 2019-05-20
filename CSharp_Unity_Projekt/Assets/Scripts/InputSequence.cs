using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSequence : MonoBehaviour
{

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
