using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CastAbility : MonoBehaviour
{
    [SerializeField]
    private GameObject basicProjectile;
    [SerializeField]
    private float shootTimerInput = 0.2f;
    [SerializeField]
    private GameObject crosshairPrefab;

    private GameObject crosshair;
    private float shootTimer;
    private GameObject projectileSpawnPoint;
    private Vector3 mousePosition;
    private Vector3 difference;
    private Vector3 differenceVariant;
    private float rotationZ;
    private float randomizer = .5f;

    private void Start()
    {
        Cursor.visible = false;
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("ProjectileSpawnPoint");
        
        new Sequence("Basic", new KeyCode[] { KeyCode.Mouse1 }); //TEMPORÄR AUF 1 STATT 0 GELEGT
        shootTimer = shootTimerInput;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //TODO: Crosshair position auf die Mausposition setzten
        if(crosshair == null)
            crosshair = Instantiate(crosshairPrefab);
        crosshair.transform.position = mousePosition;
        difference = mousePosition - projectileSpawnPoint.transform.position;
        differenceVariant = mousePosition + new Vector3(Random.Range(-randomizer, randomizer), Random.Range(-randomizer, randomizer)) - projectileSpawnPoint.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        projectileSpawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);


        //Timer, fürs schießen beim Gedrückthalten der Maustaste
        shootTimer -= Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (shootTimer <= 0)
            {
                shootTimer = shootTimerInput;
                basicShot();
            }
        }
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
        float distance = differenceVariant.magnitude;
        Vector2 direction = differenceVariant / distance;
        direction.Normalize();

        GameObject projectile = Instantiate(basicProjectile);
        projectile.transform.position = projectileSpawnPoint.transform.position;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<BasicProjectile>().moveSpeed;
    }
}
