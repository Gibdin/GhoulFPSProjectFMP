using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller; //reference to the character controller component 
    public float gravity = -30f; //how strong gravity affects the player
    public Vector3 velocity; //a vector 3 to measure the velocity of the player
    public float jumpForce = 3.25f; //the strength on the y axis when the player boosts in a jump
    

    public Transform groundCheck; //a reference to the position of the ground check
    public float groundDist = 0.7f; //the distance the raycast is fired for the groundcheck
    public LayerMask groundMask; //the layer mask for the raycast to identify
    public bool isGrounded; //a boolean check to identify if the player is or isn't grounded

    public float originalHeight; //assigning a variable of the height of the player in the character controller component
    public float reducedHeight = 1.5f; //the height that the player reduces to when input is identified to crouch 
    
    public float walkSpeed = 6.5f; //Declaring the speed the character walks in regular movement
    public float speedBoost = 5f; //the boost of speed the character increased when running
    public float currentSpeed; //a variable to track the players 


    // Start is called before the first frame update
    void Start()
    {

        groundCheck = gameObject.transform.GetChild(1);
        controller = GetComponent<CharacterController>(); //assigning the variable to the objects character controller 
        originalHeight = controller.height; //assigning the orginal height to the height assigned in the character controller component
        currentSpeed = walkSpeed; //assigning the currentSpeed to be the walkSpeed variable 
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal"); //Acquiring the input on the horizontal axis and assigning the value into a variable. The variable can be -1 for negative, 0 for neutral and 1 for positive
        float z = Input.GetAxis("Vertical"); //Acquiring the input on the vertical axis and assigning the value into a variable. The variable can be -1 for negative, 0 for neutral and 1 for positive

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) //check if the player is grounded and if the left shift button is held
        {
            currentSpeed += speedBoost; //if the statement conditions are true, the speed boost is added to the currentSpeed value 
            currentSpeed = Mathf.Clamp(currentSpeed, 7f, 12f); //The currenstSpeed is clamped to have a minimum value of 7 and a max of 12
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded) //else if the key is released and the player is grounded 
        {
            currentSpeed -= speedBoost; //if the statement is true, the current speed's value is reduced by the speedboosts value
            currentSpeed = Mathf.Clamp(currentSpeed, 7f, 12f); //the current speed is clamped again, this makes sure the value doesn't reduce or increment unneedingly.
        }


        Vector3 move = transform.right * x + transform.forward * z; //this vector 3 called move is declared and holds the value of the transform of the player on the x axis and is multipled by
        //the value held in the x variable which is between -1 for negative, 0 for neutral and 1 for positive. The transform on the z axis is repeated the same and both are added together.
        controller.Move(move * currentSpeed * Time.deltaTime); //this vector is then put into the move function in the character controller. the value is multiplied by current speed
        //which thus creates the move speed. It's also multiplied by time.deltatime to ensure that movement is increased or decreased dependant on the frame rate of the game

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //if the space bar is pressed and the player is grounded 
        {
            Jump(); //If the statement returns true, the jump function is called
        }

        if (Input.GetKey(KeyCode.LeftControl)) //if the left control key is held down 
        {
            Crouch(); //if the statement returns true, the crouch function is called
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) //if the left control key is released
        {
            StandUp(); //if the statement returns true, the stand up function is called
        }
    }

    void FixedUpdate() //in the fixed update method, the function is called at a fixed rate rather than every frame, I use this to handle the physics of the character/player
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask); //This is where a sphere is raycasted. It starts at the transform position of the ground check, and covers
        //the distance assigned in the groundDist variable, the groundMask identifies what should layer the raycast should be fired if the raycast hits, the grounded bool returns true

        if (isGrounded && velocity.y < 0) //if the boolean is true and the velocity on the y axis is less than 0
        {
            velocity.y = -2f; //if the statement returns true, the velocity on the y axis is set to -2f

        }

        velocity.y += gravity * Time.deltaTime; // velocity on the y axis is incremented by gravity on a seperate frame rate

        controller.Move(velocity * Time.deltaTime); //velocity affects the gravity of the player, increasing its force on the player over time these 2 lines of code simulate a somewhat accurate
        //simulation of gravity in the unity system
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); //This function increased the velocity through an equation of the value of jump force multiplied by -2f and by the value of gravity
        //the value of that is square rooted, thus making an accurate jump.
    }

    void Crouch()
    {
        controller.height = reducedHeight; //this function changes the controllers height to the value of the reduced height
    }

    void StandUp()
    {
        controller.height = originalHeight; //this functin changes the controllers height back to the original height assigned.
    }
}

