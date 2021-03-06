﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private ButtonController buttonController;
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
        if (buttonController.Index == index)
        {
            if (!isSlider)
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
                    pauseMenu.Resume();
                }
                else if (index == 1)
                {
                    mainMenu.OptionsMenu();
                    transform.parent.parent.GetChild(1).GetComponent<OptionsButtonController>().IsActive = true;
                }
                else if (index == 2)
                {
                    pauseMenu.BackToMain();
                    GameData.lastScene = SceneManager.GetActiveScene().buildIndex;
                }
                pressed = false;
            }
        }
    }
}
