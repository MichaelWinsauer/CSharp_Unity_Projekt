using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (!isSlider)
        {
            if (optionsButtonController.Index == index)
            {
                anim.SetBool("isSelected", true);
                if (Input.GetButton("Submit") && !pressed)
                {
                    timer = .1f;
                    pressed = true;
                }
            }
            else
            {
                anim.SetBool("isSelected", false);
            }
        }
        else
        {
            if(optionsButtonController.Index == index)
            {
                GetComponent<Image>().color = new Color(255, 134, 0, 255);

                if (Input.GetAxis("Horizontal") != 0)
                {
                    if ((GameData.options.Volume += Input.GetAxis("Horizontal") / 100) > 1)
                        GameData.options.Volume = 1;
                    else if ((GameData.options.Volume += Input.GetAxis("Horizontal") / 100) < 0)
                        GameData.options.Volume = 0;
                }
            }
            else
            {
                GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }

        if(GameData.options.UseController)
        {
            if (index == 0)
                anim.SetBool("isChoosen", true);
            else if (!isSlider)
                anim.SetBool("isChoosen", false);
        }
        else
        {
            if (index == 1)
                anim.SetBool("isChoosen", true);
            else if (!isSlider)
                anim.SetBool("isChoosen", false);
        }

        GetComponent<Image>().fillAmount = GameData.options.Volume;

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
                else if(index == 1)
                {
                    GameData.options.UseController = false;
                }
                pressed = false;
            }
        }
    }
}
