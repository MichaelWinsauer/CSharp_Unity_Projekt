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

    private float timer;
    private bool entered = false;

    private void Update()
    {
        if(timer > 0 && entered)
        {
            timer -= Time.deltaTime;
        }
        else if(entered)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(isSavepoint)
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SaveEnemies();

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SavePlayer();
            GameManager.LeftAt = leftAt;
            GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>().SetTrigger("leave");
            timer = .75f;
            entered = true;
        }

        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMovement>().Flip();
        }
    }
}
