using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    [SerializeField]
    private bool isController;
    [SerializeField]
    private Animator anim;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            anim.SetTrigger("selected");
            if(isController)
            {
                GameData.options.UseController = true;
            }
            else
            {
                GameData.options.UseController = false;
            }
            Debug.Log(GameData.options.UseController);
        }
    }
}
