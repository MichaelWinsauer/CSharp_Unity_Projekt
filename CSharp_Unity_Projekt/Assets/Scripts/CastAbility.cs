using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour
{

    private Transform projectileSpawnPoint;
    public GameObject basicProjectile;

    private void Start()
    {
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("ProjectileSpawnPoint").transform;
        new Sequence("Basic", new KeyCode[] { KeyCode.J });
    }

    internal void checkAbility(string name)
    {
        switch(name)
        {
            //Alle Einzelnen Fähigkeiten
            case "Basic":
                basicShot();
                break;
        }
    }

    private void basicShot()
    {
        Instantiate(basicProjectile, projectileSpawnPoint.transform.position, transform.rotation);
    }
}
