using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position;//powerup follos the player
    }
    private void OnTriggerEnter(Collider other)//for info 
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);//sets visibility of poerup indicator
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());//starts the routine
        }
    }
    IEnumerator PowerupCountdownRoutine()//in this case sets a timer outside update method
    {
        yield return new WaitForSeconds(7);//after time it will do things
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);//hides powerup indicator
    }
    private void OnCollisionEnter(Collision collision)//for physics interaction
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;//direction where the force will shoot

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);//force when collision
            Debug.Log("choco con "+ collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
