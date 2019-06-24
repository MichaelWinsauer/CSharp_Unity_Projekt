using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private bool gamePaused;

    public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    public void Update()
    {
        if(pauseMenu.activeSelf)
        {
            if (Input.GetButtonDown("Start") || Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }
        }
        else
        {
            if (Input.GetButtonDown("Start"))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenu.activeSelf)
        {
            if(pauseMenu.transform.GetChild(0).gameObject.activeInHierarchy)
                Resume();
        }
        else
        {
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
    }
}
