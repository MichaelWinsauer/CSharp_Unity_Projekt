using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float[] position;
    public GameObject player;


    public PlayerData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}
