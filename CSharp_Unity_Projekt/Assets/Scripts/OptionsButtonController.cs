using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtonController : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private int maxIndex;

    private int index;
    private bool keyDown;
    private float timer;
    private bool pressed = false;
    private bool isActive;

    public int Index { get => index; set => index = value; }
    public bool IsActive { get => isActive; set => isActive = value; }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                        index++;
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
            }
            keyDown = true;
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            
            GetComponent<Animator>().SetTrigger("close");
            isActive = false;
            timer = 1.1f;
            pressed = true;
        }
        else
        {
            keyDown = false;
        }

        if (timer > 0 && pressed)
            timer -= Time.deltaTime;
        else if (pressed)
        {
            mainMenu.GoBack();
            pressed = false;
        }
    }
}
