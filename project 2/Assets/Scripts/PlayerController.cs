using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed;

    private float xRange = 10.0f;
    private float zRangeMin = 0f;
    private float zRangeMax = 15.0f;
    private float zSpaceProjectil = 1.5f;

    public GameObject projectilePrefab;

    static public int score = 0;
    static public int livesPlayer = 3;

    private void Start()
    {
        Debug.Log("Lives: 3   Score: 0");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        if (transform.position.x < -xRange || transform.position.x > xRange)
        {
            transform.position = new Vector3(transform.position.x < -xRange ? -xRange : xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zRangeMin || transform.position.z > zRangeMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z < zRangeMin ? zRangeMin : zRangeMax);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + zSpaceProjectil), projectilePrefab.transform.rotation);
        }

        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Over!");
    }*/


    
}
