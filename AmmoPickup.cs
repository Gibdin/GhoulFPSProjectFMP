using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour //this is an ammo pick up class for whenever the player collides with an ammo pick up and depending on the box, adds an amount of ammo to an ammo type
{
    public int ammountPickedUp = 10; //this is a public int variable for the ammount of ammo added when the box is picked up, this is changed for each prefab of the ammo box so it isnt the same
    //amount
    public AmmoType ammoType; //this is a variable of the ammo type class referenced as ammo type

    public void OnTriggerEnter(Collider player) //this is a method which requires a collider trigger component on the object, if anything enters the trigger collider this method is executed
        //and takes in the whatever collided with it 
    {
        if (player.gameObject.tag == "Player") //this if statement checks what the collision was with, using the unity tags and searching if the collider was the player as its the only one assigned
            //the player tag the if statement is played out
        {
            FindObjectOfType<Ammo>().IncreaseAmmo(ammoType, ammountPickedUp); //the ammo class is located and the increase ammo function is executed, taking in the ammo type and ammount picked up
            //variables 
            Destroy(gameObject); //the pick up is then destroyed
        }
    }
}
