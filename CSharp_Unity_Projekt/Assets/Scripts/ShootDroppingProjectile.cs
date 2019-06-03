using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDroppingProjectile : MonoBehaviour
{
    [SerializeField]
    private float cooldownInput;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float hoverDurationInput;

    private GameObject player;
    private Movement playerMovement;
    private GameObject spawnPoint;
    private float cooldown;
    private GameObject projectileS;
    private GameObject projectileM;
    private GameObject projectileL;
    private DroppingProjectile projectileProperties;
    private float hoverDuration;
    private float gravityScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement>();
        spawnPoint = GameObject.FindGameObjectWithTag("ProjectileSpawnPoint");
        projectileProperties = projectilePrefab.GetComponent<DroppingProjectile>();
        gravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (!playerMovement.IsGrounded)
            if (cooldown <= 0)
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    cooldown = cooldownInput;
                    hoverDuration = hoverDurationInput;
                    //shoot();
                }

        hover();
    }

    private void hover()
    {
        if (hoverDuration >= 0)
        {
            hoverDuration -= Time.deltaTime;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }

        if (hoverDuration >= 0 && hoverDuration < 0.1 && GameObject.FindGameObjectsWithTag("Projectile").Length < 3)
            shoot();
    }

    private void shoot()
    {
        projectileS = Instantiate(projectilePrefab, spawnPoint.transform);
        projectileM = Instantiate(projectilePrefab, spawnPoint.transform);
        projectileL = Instantiate(projectilePrefab, spawnPoint.transform);

        projectileS.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileProperties.Speed, projectileS.GetComponent<Rigidbody2D>().velocity.y);
        projectileM.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileProperties.Speed * 1.5f, projectileS.GetComponent<Rigidbody2D>().velocity.y);
        projectileL.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileProperties.Speed * 2f, projectileS.GetComponent<Rigidbody2D>().velocity.y);

    }
}
