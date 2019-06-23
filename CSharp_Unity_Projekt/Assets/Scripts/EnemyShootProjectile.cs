using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float frequencyInput;

    private GameObject player;
    private GameObject projectileSpawnPoint;
    private GameObject projectile;
    private EnemyProjectile projectileProperties;
    private float frequency;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawnPoint = GameObject.FindGameObjectWithTag("EnemyProjectileSpawnpoint");
    }

    // Update is called once per frame
    void Update()
    {
        frequency -= Time.deltaTime;
        if(frequency <= 0)
        {

            projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
            projectileProperties = projectile.GetComponent<EnemyProjectile>();
            frequency = frequencyInput;
            
        }
    }
}
