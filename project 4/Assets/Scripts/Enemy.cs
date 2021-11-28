using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public int enemyLevel;
    public bool isBoss = false;
    public float spawnInterval;
    public int miniEnemySpawnCount;

    private Rigidbody enemyRb;
    private GameObject player;
    private float enemyBoost;
    private float nextSpawn;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyBoost = 1 + ((enemyLevel - 1) / 2);

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;//normalized: normalize the magnitud of vector so at any distance the speed is the same

        enemyRb.AddForce(lookDirection * speed * enemyBoost);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }        
    }
}
