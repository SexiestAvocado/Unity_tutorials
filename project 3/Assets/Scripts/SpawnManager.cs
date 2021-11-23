using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;
    private int whichObstacle;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float timer;
    private float timeBeforeNextObstacle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            whichObstacle = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[whichObstacle], spawnPosition, obstaclePrefabs[whichObstacle].transform.rotation);
            //1 chance out of 2 to spawn the same obstacle on top of the first one
            if (Random.Range(0, 2) == 1)
            {
                GameObject instantiatedObject = obstaclePrefabs[whichObstacle];
                float additionalHeight = instantiatedObject.GetComponent<BoxCollider>().size.y;
                Instantiate(obstaclePrefabs[whichObstacle], new Vector3(spawnPosition.x, spawnPosition.y + additionalHeight, spawnPosition.z), obstaclePrefabs[whichObstacle].transform.rotation);
                //1chance out of 6 ot have a 3stacked obstacle
                if (Random.Range(0, 3) == 1)
                {
                    Instantiate(obstaclePrefabs[whichObstacle], new Vector3(spawnPosition.x, spawnPosition.y + 2 * additionalHeight, spawnPosition.z), obstaclePrefabs[whichObstacle].transform.rotation);
                }
            }
            timer = Time.time;
            timeBeforeNextObstacle = Random.Range(1.0f, 2.0f);
        }
    }


}
    /*GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    Vector3 spawnPosition = obstaclePrefab.transform.position;

    if (playerControllerScript.gameOver == false)
    {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    } */
    