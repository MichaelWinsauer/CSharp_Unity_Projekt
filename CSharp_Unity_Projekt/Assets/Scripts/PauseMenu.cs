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
        if (Input.GetButtonDown("Start"))
        {
            PauseGame();
        }
        Debug.Log(GetComponent<Movement>().CanMove);
    }

    public void PauseGame()
    {
        if (pauseMenu.activeSelf == true)
        {
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
