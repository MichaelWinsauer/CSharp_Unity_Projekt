using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private ButtonController buttonController;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int index;
    [SerializeField]
    private bool isOptions;
    [SerializeField]
    private bool isSlider;

    private float timer;
    private bool pressed = false;
    // Update is called once per frame
    void Update()
    {
        if(buttonController.Index == index)
        {
            if(!isSlider)
            {
                anim.SetBool("isSelected", true);
                if (Input.GetButton("Submit"))
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

        doSomething();
    }

    private void doSomething()
    {
        if(!isOptions)
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
                        mainMenu.PlayGame();
                    }
                    else if (index == 1)
                    {
                        mainMenu.OptionsMenu();
                    }
                    else if (index == 2)
                    {
                        mainMenu.QuitGame();
                    }
                    pressed = false;
                }
            }
        }
        else
        {
            if(pressed)
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
                        GameData.options.UseController = true;
                    }
                    pressed = false;
                }
            }
        }
    }
}
