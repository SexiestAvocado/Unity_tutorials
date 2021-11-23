using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private PlayerController playerControllerScript;
    private float leftBound = -9;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); //borrows the script
    }

    // Update is called once per frame
    void Update()
    {
        //bool gameOver is borrowed from script
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.doubleSpeed)//accelerates when dash is active
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))//destroys out of bounds obstacles
        {
            Destroy(gameObject);
        }
    }
}
