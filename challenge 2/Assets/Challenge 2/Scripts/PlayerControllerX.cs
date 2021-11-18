using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float timeToWait = 2;
    public float timeCount = 0;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > timeCount)
        {
            timeCount = Time.time + timeToWait;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
