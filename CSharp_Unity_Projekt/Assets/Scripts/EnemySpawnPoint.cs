using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint
{
    private GameObject enemy;
    private Vector3 position;
    public GameObject Enemy { get => enemy; set => enemy = value; }
    public Vector3 Position { get => position; set => position = value; }

    public EnemySpawnPoint(GameObject enemy, Vector3 position)
    {
        this.enemy = enemy;
        this.position = position;
    }
}
