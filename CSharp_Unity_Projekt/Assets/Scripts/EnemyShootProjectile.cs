using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private float shootTimerInput;

    private GameObject player;
    private GameObject projectileSpawnPoint;
    private float shootTimer;
    private bool canShoot;

    public bool CanShoot { get => canShoot; set => canShoot = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("EnemyProjectileSpawnpoint");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPoint = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform.position;
        Vector2 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (shootTimer <= 0)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            if(canShoot)
            {
                GetComponentInChildren<Animator>().SetTrigger("shoot");
                GameObject projectile = Instantiate(enemyProjectile, spawnPoint, Quaternion.Euler(0f, 0f, rotationZ));
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<EnemyProjectile>().MoveSpeed * Random.Range(.5f, 1.5f);
                projectile.GetComponent<EnemyProjectile>().Rotation = rotationZ;
                shootTimer = shootTimerInput * Random.Range(.5f, 1.5f);
            }

        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }
}
