using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 320.0f;
    private Rigidbody PlayerRb;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //use rigidbody to move applying force
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //method 1. doesn´t work if kinematic. !!!USE THIS IF ONLY MOVING WITH KEYBOARD. DON´T COMBINE BOTH TYPES AT SAME TYPE
        if(!PlayerRb.isKinematic)
        {
            PlayerRb.AddForce(Vector3.forward * speed * verticalInput);
            PlayerRb.AddForce(Vector3.right * speed * horizontalInput);
        }
        //method 2. doesn´t work if dynamic. works worse but save it if needed
        /*else if(PlayerRb.isKinematic)
        {
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }*/
    }

}
