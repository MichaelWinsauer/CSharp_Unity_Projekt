using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Das Spielerobjekt wird im Projekt gesucht und es wird eine Referenz dazu gebildet.
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Die Kameraposition wird auf die des Spielers gesetzt. .Lerp ist eine Funktion, die langsahm von einer Position auf die nächste beschleunigt.
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, position + new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY) / 2, player.position.z), speed);
    }
}
