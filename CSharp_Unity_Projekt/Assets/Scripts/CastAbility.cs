using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour
{
    public GameObject basicProjectile;

    private GameObject projectileSpawnPoint;
    private Vector3 mousePosition;
    private Vector3 difference;
    private float rotationZ;

    private void Start()
    {
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("ProjectileSpawnPoint");
        new Sequence("Basic", new KeyCode[] { KeyCode.Mouse0 });
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //TODO: Crosshair position auf die Mausposition setzten

        difference = mousePosition - projectileSpawnPoint.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        projectileSpawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void checkAbility(string name)
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
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        GameObject projectile = Instantiate(basicProjectile);
        projectile.transform.position = projectileSpawnPoint.transform.position;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<BasicProjectile>().moveSpeed;
    }
}
