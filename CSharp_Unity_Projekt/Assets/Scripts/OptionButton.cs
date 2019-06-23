using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private OptionsButtonController optionsButtonController;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int index;
    [SerializeField]
    private bool isSlider;

    private float timer;
    private bool pressed = false;
    // Update is called once per frame
    void Update()
    {
        if (optionsButtonController.Index == index)
        {
            if (!isSlider)
            {
                anim.SetBool("isSelected", true);
                if (Input.GetButton("Submit") && !pressed)
                {
                    anim.SetTrigger("pressed");
                    timer = .1f;
                    pressed = true;
                }
            }
        }
        else
        {
            anim.SetBool("isSelected", false);
        }

        if(GameData.options.UseController)
        {
            if (index == 0)
                anim.SetBool("isChoosen", true);
            else
                anim.SetBool("isChoosen", false);
        }
        else
        {
            if (index == 1)
                anim.SetBool("isChoosen", true);
            else
                anim.SetBool("isChoosen", false);
        }

        doSomething();
    }

    private void doSomething()
    {
        if (pressed)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (index == 0)
                {
                    GameData.options.UseController = true;
                }
                else if (index == 1)
                {
                    GameData.options.UseController = false;
                }
                pressed = false;
            }
        }
        Debug.Log(GameData.options.UseController);
    }
}
