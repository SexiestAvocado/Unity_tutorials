using UnityEngine;

public class DragMouseMove : MonoBehaviour
{
    //!!!USE THIS IF KINEMATIC, DON´T COMBINE WITH THE OTHER SCRIP OF KEYBOARD
    private Plane draggingPlane;//This is where we store the Plane for dragging objects
    private Vector3 offset;//This will store the difference between where the mouse is clicked on the Plane and where the origin of the object is
    private Camera mainCamera;//This will be used to cache to main camera You could also use a serialized field to accomplish the same thing
    private Rigidbody playerRb;
   
    // Start is called before the first frame update
    void Start()
    {
        // Cache the camera at the start. 
        mainCamera = Camera.main;
        playerRb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if(playerRb.isKinematic)
        {
            draggingPlane = new Plane(mainCamera.transform.forward, transform.position);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDistance;
            draggingPlane.Raycast(camRay, out planeDistance);
            offset = transform.position - camRay.GetPoint(planeDistance);
        }
    }
    void OnMouseDrag()
    {
        if(playerRb.isKinematic)
        {
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDistance;
            draggingPlane.Raycast(camRay, out planeDistance);
            //transform.position = camRay.GetPoint(planeDistance) + offset;
            playerRb.MovePosition(camRay.GetPoint(planeDistance) + offset);//this works better for interacting with other objects
        }
    }
}
