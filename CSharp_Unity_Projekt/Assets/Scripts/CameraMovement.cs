using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public Vector3 position = new Vector3(2f, 2.5f, -1f);
    public Transform player;
    public float speed = 0.15f;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, position + player.position, speed);
    }
}
