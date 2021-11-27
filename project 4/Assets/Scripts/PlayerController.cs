using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public bool hasBullets = false;
    public GameObject powerupIndicator;
    public GameObject bulletsIndicator;
    public PowerupType currentPowerup = PowerupType.None;
    public GameObject bulletPrefab;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;
    private GameObject tmpBullet;
    private Coroutine powerupCountdown;

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
        //bulletsIndicator.transform.position = transform.position;

        if (currentPowerup == PowerupType.Bullets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchBullets();
        }
    }
    private void OnTriggerEnter(Collider other)//for info 
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerupType;
            powerupIndicator.gameObject.SetActive(true);//sets visibility of powerup indicator
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());//starts the routine

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
        /*if (other.CompareTag("Bullet Powerup"))
        {
            hasBullets = true;
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerupType;
            bulletsIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(BulletsCountdownRoutine());            
        }*/
    }
    IEnumerator PowerupCountdownRoutine()//in this case sets a timer outside update method
    {
        yield return new WaitForSeconds(7);//after time it will do things
        hasPowerup = false;
        currentPowerup = PowerupType.None;
        powerupIndicator.gameObject.SetActive(false);//hides powerup indicator
    }

    /*IEnumerator BulletsCountdownRoutine()
    {
        yield return new WaitForSeconds(5);//after time it will do things
        hasBullets = false;
        currentPowerup = PowerupType.None;
        bulletsIndicator.gameObject.SetActive(false);//hides powerup indicator
    }*/
    private void OnCollisionEnter(Collision collision)//for physics interaction
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup && currentPowerup == PowerupType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;//direction where the force will shoot

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);//force when collision
            
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerup.ToString());
        }
    }
    void LaunchBullets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpBullet = Instantiate(bulletPrefab, transform.position + Vector3.up,
            Quaternion.identity);
            tmpBullet.GetComponent<BulletBehaviour>().Fire(enemy.transform);
        }
    }

}
