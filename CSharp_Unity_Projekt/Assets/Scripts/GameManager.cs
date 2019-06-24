using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ranged;
    [SerializeField]
    private GameObject melee;

    private GameObject player;
    private GameObject cam;
    private string currentScene;
    private static PlayerPosition leftAt;

    public static PlayerPosition LeftAt { get => leftAt; set => leftAt = value; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("CameraObject");
        currentScene = ((Scene)SceneManager.GetActiveScene()).name;

        //Festlegen der ganzen Spielerstats.
        if (GameData.player == null)
        {
            GameData.player = new PlayerData();
            GameData.player.Health = player.GetComponent<PlayerHealth>().MaxHealth;
            GameData.player.CanDash = player.GetComponent<PlayerDash>().CanDash;
        }

        if (GameData.options == null)
        {
            GameData.options = new OptionsData(true, .5f);
        }

        player.GetComponent<PlayerHealth>().CurrentHealth = GameData.player.Health;
        player.GetComponent<PlayerDash>().CanDash = GameData.player.CanDash;

        //Festlegen der Anzahl und Position der Gegner.
        CheckEnemies();
        movePlayer();
    }

    private void movePlayer()
    {
        switch(leftAt)
        {
            case PlayerPosition.Spawn:
                player.transform.position = new Vector3(-38.43f, -5.36f);
                break;

            case PlayerPosition.OneRight:
                player.transform.position = new Vector3(-79.77f, 2.228f);
                break;

            case PlayerPosition.TwoLeft:
                player.transform.position = new Vector3(120.28f, 2.73f);
                cam.transform.position = new Vector3(100.85f, 5.47f / 2);
                break;

            case PlayerPosition.TwoRight:
                player.transform.position = new Vector3(-80.15f, -2.81f);
                break;

            case PlayerPosition.ThreeLeft:
                player.transform.position = new Vector3(80.53f, -2.67f);
                cam.transform.position = new Vector3(60.5f, -3.4f / 2);
                break;

            case PlayerPosition.ThreeRight:
                player.transform.position = new Vector3(-79.78f, 5.87f);
                break;

            case PlayerPosition.ThreeTop:
                player.transform.position = new Vector3(38.64f, -6.83f);
                break;

            case PlayerPosition.FourRight:
                player.transform.position = new Vector3(-80.15f, 15.49f);
                cam.transform.position = new Vector3(-80.15f, 15.49f / 2);
                break;

            case PlayerPosition.FourBottom:
                player.transform.position = new Vector3(26f, 11.5f);
                cam.transform.position = new Vector3(26f, 11.5f / 2);
                break;

            case PlayerPosition.FiveLeft:
                player.transform.position = new Vector3(79.64f, 4.91f);
                cam.transform.position = new Vector3(60.8f, 16.07f / 2);
                break;

            case PlayerPosition.FiveRight:
                player.transform.position = new Vector3(-79.78f, 5.87f);
                break;
        }
    }

    public void SavePlayer()
    {
        GameData.player.CanDash = player.GetComponent<PlayerDash>().CanDash;
        GameData.player.Health = player.GetComponent<PlayerHealth>().CurrentHealth;
    }

    public void CheckEnemies()
    {
        switch (currentScene)
        {
            case "Level 1":
                if (GameData.levelOne != null)
                {
                    spawnEnemies(GameData.levelOne);
                }
                break;

            case "Level 2":
                if (GameData.levelTwo != null)
                {
                    spawnEnemies(GameData.levelTwo);
                }
                break;

            case "Level 3":
                if (GameData.levelThree != null)
                {
                    spawnEnemies(GameData.levelThree);
                }
                break;

            case "Level 4":
                if (GameData.levelFour != null)
                {
                    spawnEnemies(GameData.levelFour);
                }
                if (GameData.player.CanDash)
                    Destroy(GameObject.FindGameObjectWithTag("DashItem"));
                break;

            case "Level 5":
                if (GameData.levelFive != null)
                {
                    spawnEnemies(GameData.levelFive);
                }
                break;
        }
    }

    public void SaveEnemies()
    {
        switch (currentScene)
        {
            case "Level 1":
                GameData.levelOne = createEnemyList();
                break;

            case "Level 2":
                GameData.levelTwo = createEnemyList();
                break;

            case "Level 3":
                GameData.levelThree = createEnemyList();
                break;

            case "Level 4":
                GameData.levelFour = createEnemyList();
                break;

            case "Level 5":
                GameData.levelFive = createEnemyList();
                break;
        }
    }

    private List<EnemyData> createEnemyList()
    {
        List<EnemyData> localEnemies = new List<EnemyData>();
        
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            localEnemies.Add(new EnemyData(enemy.GetComponent<EnemyMovement>().IsRanged, enemy.GetComponent<Enemy>().Health, enemy.transform.position.x, enemy.transform.position.y));
        }

        return localEnemies;
    }

    private void spawnEnemies(List<EnemyData> data)
    {
        GameObject enemy;
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(e);
        }

        foreach (EnemyData e in data)
        {
            if(e.IsRanged)
            {
                enemy = Instantiate(ranged);
            }
            else
            {
                enemy = Instantiate(melee);
            }

            enemy.GetComponent<Enemy>().Health = e.Health;
            enemy.transform.position = new Vector2(e.PosX, e.PosY + 1.5f);
        }
    }
}
