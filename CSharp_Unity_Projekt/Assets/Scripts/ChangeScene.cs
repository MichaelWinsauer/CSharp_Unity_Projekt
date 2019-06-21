using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private PlayerPosition leftAt;
    [SerializeField]
    private bool isSavepoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(isSavepoint)
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SaveEnemies();

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SavePlayer();
            GameManager.LeftAt = leftAt;
            SceneManager.LoadScene(nextScene);
        }
    }
}
