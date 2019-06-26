using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 position = new Vector3(2f, 2.5f, -1f);
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    private float speed = 0.15f;
    private Transform player;
    private string currentScene;


    //Das Spielerobjekt wird im Projekt gesucht und es wird eine Referenz dazu gebildet.
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        currentScene = ((Scene)SceneManager.GetActiveScene()).name;

        if (Screen.currentResolution.width == 3000)
        {
            switch(currentScene)
            {
                case "Level 1":
                    minX = -28.49f;
                    maxX = 104.5f;
                    break;

                case "Level 2":
                    minX = -68.6f;
                    break;

                case "Level 3":
                    minX = -66.37f;
                    break;

                case "Level 4":
                    minX = -40.6f;
                    maxX = 23.4f;
                    break;

                case "Level 5":
                    minX = -68.51f;
                    maxX = 64.5f;
                    break;

                case "Level 6":

                    break;
            }
        }
    }

    private void Update()
    {
        AudioListener.volume = GameData.options.Volume;
    }


    // Die Kameraposition wird auf die des Spielers gesetzt. .Lerp ist eine Funktion, die langsahm von einer Position auf die nächste beschleunigt.
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, position + new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY) / 2, player.position.z), speed);
    }
}
