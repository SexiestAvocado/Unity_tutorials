using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject[] powerupPrefabs;

    private float spawnRange = 9.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        //Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;//returns lenght of the array
        //enemyCount = GameObject.FindGameObjectWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();

            /*int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);*/
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
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
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);

        Vector3 randomPosPowerup = new Vector3(powerupPosX, 0, powerupPosZ);

        Instantiate(powerupPrefabs[randomPowerup], randomPosPowerup, powerupPrefabs[randomPowerup].transform.rotation);
    }
}
