using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 30.0f;
    public float horizontalInput;
    public float verticalInput;
    public Camera mainCamera; //variables for main camera in 3rd person
    public Camera hoodCamera; //variable for camera in first person
    public KeyCode switchKey; //variable for changing camera
    public string inputID; //to determine wich player is using the script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        verticalInput = Input.GetAxis("Vertical" + inputID);

        //moving forward
        //transform.Translate(0, 0, 1); the same thing as above
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        //turn vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        //toggle which camera is enabled whenthe F key is pressed
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
