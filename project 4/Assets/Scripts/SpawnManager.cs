using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;

    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;//returns lenght of the array

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 10, spawnPosZ);

        return randomPos;
    }
    void SpawnPowerup()
    {
        float powerupPosX = Random.Range(-spawnRange, spawnRange);
        float powerupPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPosPowerup = new Vector3(powerupPosX, 0, powerupPosZ);

        Instantiate(powerupPrefab, randomPosPowerup, powerupPrefab.transform.rotation);
    }
}
