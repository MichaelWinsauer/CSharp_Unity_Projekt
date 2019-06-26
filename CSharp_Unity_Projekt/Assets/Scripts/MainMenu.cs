using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;

    private void Start()
    {
        //Marcel
        FindObjectOfType<AudioManager>().Play("MainMenu");

        if (GameData.options == null)
        {
            GameData.options = new OptionsData(true, .5f);
        }
    }

    private void FixedUpdate()
    {
        if(FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume -= 0.001f;
        }

        if (FindObjectOfType<AudioManager>().GetSource("IdleTheme").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("IdleTheme").volume -= 0.001f;
        }

        if (FindObjectOfType<AudioManager>().GetSource("CreditsTheme").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("CreditsTheme").volume -= 0.001f;
        }
    }

    public void PlayGame()
    {
        if (GameData.lastScene == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(GameData.lastScene);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
