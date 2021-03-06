﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject basicProjectile;
    [SerializeField]
    private float shootTimerInput = 0.2f;

    private float shootTimer;
    private GameObject projectileSpawnPoint;
    private Vector3 mousePosition;
    private Vector3 difference;
    private Vector3 differenceVariant;
    private float rotationZ;
    private float randomizer = .5f;

    //Projektielreferenzen aufgebaut und Timer gesetzt.
    private void Start()
    {
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("ProjectileSpawnPoint");
        shootTimer = shootTimerInput;
    }

    //Erst wird die Mausposition abgefragt.
    //Danach wird der Vektor aus Erzeugungspunkt und Mausposition errechnet. Hier wird eine Varianz draufgerechnet, damit man nicht punktgenau schießen kann.
    //Anschließend wird die Rotation des Projektiels auf den Winkel zwischen der X-Achse und dem Mauspunkt berechnet. 
    //Dann wird lediglich ein Timer verwendet, damit nicht 60 Projektile pro Sekunde erzeugt werden.
    private void Update()
    {
        if(!GetComponent<PlayerHealth>().IsDead)
        {
            if(!GameData.options.UseController)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                difference = mousePosition - projectileSpawnPoint.transform.position;
                differenceVariant = mousePosition + new Vector3(Random.Range(-randomizer, randomizer), Random.Range(-randomizer, randomizer)) - projectileSpawnPoint.transform.position;
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                projectileSpawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

                shootTimer -= Time.deltaTime;
                if (Input.GetButton("Fire1"))
                {
                    if (shootTimer <= 0)
                    {
                        shootTimer = shootTimerInput;
                        basicShot();
                    }
                }
            }
            else
            {
                Vector3 controllerPoint = new Vector3(
                projectileSpawnPoint.transform.position.x + Input.GetAxis("HorizontalAim"),
                projectileSpawnPoint.transform.position.y + Input.GetAxis("VerticalAim"));

                difference = controllerPoint - projectileSpawnPoint.transform.position;
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                projectileSpawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

                shootTimer -= Time.deltaTime;
                if ((Input.GetAxis("HorizontalAim") != 0 || Input.GetAxis("VerticalAim") != 0) && Input.GetButton("Fire1"))
                {
                    if (shootTimer <= 0)
                    {
                        shootTimer = shootTimerInput;
                        basicShot();
                    }
                }
            }            
        }
    }

    //Hier wird das Projektiel erstellt.
    //Da man die Bewegungsgeschwindigkeit festlegen muss, da man dafür 2 Vektoren angeben muss, muss der Vektor aus Erzeugungspunkt und Mausposition erst normalisiert werden.
    //Wenn man das nicht macht, bewegt sich das Projektiel schneller wenn man weiter vom Spieler entfernt drückt. So hat das Projektiel immer die gleiche Geschwindigkeit.
    private void basicShot()
    {
        //Marcel
        //FindObjectOfType<AudioManager>().Play("SpellCast");

        Vector2 direction;

        if (!GameData.options.UseController)
        {
            float distance = differenceVariant.magnitude;
            direction = differenceVariant / distance;
            direction.Normalize();
        }
        else
        {
            direction = new Vector2(Input.GetAxis("HorizontalAim"), Input.GetAxis("VerticalAim"));
            direction.Normalize();
        }

        GameObject projectile = Instantiate(basicProjectile);
        projectile.transform.position = projectileSpawnPoint.transform.position;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<BasicProjectile>().MoveSpeed;
        projectile.GetComponent<BasicProjectile>().Rotation = rotationZ;
    }
}
