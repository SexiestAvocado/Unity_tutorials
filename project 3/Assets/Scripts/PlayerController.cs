using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public float jumpForce = 12;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; //to use gravity 
    }

    // Update is called once per frame
    void Update()
    {
        //jumping when player is on the ground running
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)//!bool is the same as bool == false or bool != true
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);     
        }
        //double jumping when player is on air
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed /*&& !gameOver*/)
        {
            playerRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpUsed = true;
        }
        //let more than one double jump
        if (isOnGround)
        {
            doubleJumpUsed = false;
        }
        //dash ability
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        //animation on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        //things when dead
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("game over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

        
    }
}
