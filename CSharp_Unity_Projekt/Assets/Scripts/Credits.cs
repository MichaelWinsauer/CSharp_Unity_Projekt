using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 65f;
        FindObjectOfType<AudioManager>().GetSource("CreditsTheme").volume = 0.2f;
        FindObjectOfType<AudioManager>().Play("CreditsTheme");

        resetGameData();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("BattleTheme").volume -= 0.001f;
        }

        if (FindObjectOfType<AudioManager>().GetSource("IdleTheme").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("IdleTheme").volume -= 0.001f;
        }
    }

    private void resetGameData()
    {
        GameData.player = null;
        GameData.levelOne = null;
        GameData.levelTwo = null;
        GameData.levelThree = null;
        GameData.levelFour = null;
        GameData.levelFive = null;
        GameManager.LeftAt = PlayerPosition.Spawn;
        GameData.lastScene = 0;
        GameData.deathCount = 0;
    }

}
