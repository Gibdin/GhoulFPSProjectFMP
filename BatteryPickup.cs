using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour //the battery pick up script is for the flashlight when interacting with a pick up
{
    public float restoreAngle = 30f; //the float value of the angle restored for the flashlight 
    public float restoreIntensity = 15f; //the float value of the intensity restored for the flashlight 

    private void OnTriggerEnter(Collider other) //this is a method for a trigger collider, if anything enters or interacts with the collider, the method is played and whatever collider interacted
        //with the collider is stored into the variable other
    {
        if (other.gameObject.tag == "Player") //if the other variable shares the game object tag of the player we can recognize to continue this script as the flashlight is on the player 
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngle); //the flashlight component is stored on the main camera which is a child object on the player
            //therefore we use get component in children and get the flashlight class to access the two functions restore light and light intensity passing in the two variables
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(restoreIntensity);
            Destroy(gameObject); //we then destroy the pick up object. 
        }
    }
}
