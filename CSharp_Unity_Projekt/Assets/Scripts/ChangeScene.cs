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

    private void FixedUpdate()
    {
        if(FindObjectOfType<AudioManager>().GetSource("MainMenu").isPlaying && FindObjectOfType<AudioManager>().GetSource("MainMenu").volume > 0f)
        {
            FindObjectOfType<AudioManager>().GetSource("MainMenu").volume = FindObjectOfType<AudioManager>().GetSource("MainMenu").volume -0.001f;
            if(FindObjectOfType<AudioManager>().GetSource("MainMenu").volume == 0f)
            {
                FindObjectOfType<AudioManager>().GetSource("MainMenu").Stop();
                FindObjectOfType<AudioManager>().GetSource("MainMenu").volume = 0.3f;
            }
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
            if(collision.GetComponent<Enemy>().IsRanged)
                collision.gameObject.GetComponent<EnemyMovementRanged>().Flip();
            else
                collision.gameObject.GetComponent<EnemyMovement>().Flip();
        }
    }
}
