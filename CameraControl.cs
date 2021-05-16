using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float mouseSens = 300f; //Declaring the speed of the mouse moving the camera
    public Transform pBody; //reference to the actual body of the player. 

    private float xRotation = 0f; //recording the rotation recorded of camera movements around the x axis. 

    public bool flashLightEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks the camera on game start so the cursor can not tab out the game
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; //defines the variable of mouse X to be the input of any movement on the x axis on the mouse. 
        //This input result is possible to be between -1 for negative, 0 for neutral and 1 for positive. this result is then multiplied by the mousSens, increasing the pixels moved on the screen.
        //this is then multiplied by time.deltatime to make sure the speed of the mouse isn't increased due to the frame rate of the project.
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime; //defines the variable of mouse X to be the input of any movement on the x axis on the mouse. 
        //This input result is possible to be between -1 for negative, 0 for neutral and 1 for positive. this result is then multiplied by the mousSens, increasing the pixels moved on the screen.
        //this is then multiplied by time.deltatime to make sure the speed of the mouse isn't increased due to the frame rate of the project.

        xRotation -= mouseY; //the rotation around the x axis is reduced by the value of mouseY, 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //this value is then clamped between -90 and 90 degrees. This means the camera can not look behind them through looking up or down.

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //the transform's local rotation is then rotated dependant on the value of xrotation on the z axis.
        pBody.Rotate(Vector3.up * mouseX); //the body of the player rotates depending on the movement of the mouse on the x axis, the vector 3 is multiplied by either -1 for left, 0 for middle and 1 for right

    }

    
}
