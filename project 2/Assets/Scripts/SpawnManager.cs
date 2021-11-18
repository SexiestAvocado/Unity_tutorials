using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    const int TOP_ROTATION = 0, LEFT_ROTATION = 1, RIGHT_ROTATION = 2;

    public GameObject[] animalPrefabs;

    public float spawnRangeX = 20;
    public float spawnPosZMax = 15;
    public float spawnPosZMin = 0;
    public float startDelay = 1.7f;
    public float spawnInterval = 1.1f;

    void Start()
    {
        //InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        int rotationType = Random.Range(0, 3);
        Vector3 spawnPosition = animalPrefab.transform.position;
        Quaternion spawnRotation = animalPrefab.transform.rotation;

        switch (rotationType)
        {
            case TOP_ROTATION:
                spawnRotation = Quaternion.Euler(0, 180, 0);
                spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZMax);
                break;
            case LEFT_ROTATION:
                spawnRotation = Quaternion.Euler(0, 90, 0);
                spawnPosition = new Vector3(-spawnRangeX, 0, Random.Range(-spawnPosZMin, spawnPosZMax));
                break;
            case RIGHT_ROTATION:
                spawnRotation = Quaternion.Euler(0, 270, 0);
                spawnPosition = new Vector3(spawnRangeX, 0, Random.Range(-spawnPosZMin, spawnPosZMax));
                break;
        }

        Instantiate(animalPrefab, spawnPosition, spawnRotation);
    }
}