using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots; //I was taught this function from a peer, a serialize field is a non public nor private version of creating a reference to another class, script, component
    //game object or a variable, I created an array labelled AmmoSlot which work off index numbers, the array is labelled the variable ammoSlots

    [System.Serializable] //because the ammo slot class is a prviate class we have to make it public using system.serializable so it can be accessed in the script and in other scripts
    private class AmmoSlot 
    {
        public AmmoType ammoType; //the private class holds 2 variables, a variable of the ammo type class referenced as ammoType and an integer value labelled ammo amount, we use this to track what 
        //weapon uses what ammo type and the ammount of ammo it has left 
        public int ammoAmmount;
    }
    public int GetCurrentAmmo(AmmoType ammoType) //This is a public function which returns an integer of how much ammo is currently inside a weapon, it takes in the ammo type class in the function
    {
        return GetAmmoSlot(ammoType).ammoAmmount; //it returns the ammo amount by using the get ammo slot function passing in the ammo type and getting the ammo amount left for that ammo type
    }

    public void ReduceCurrentAmmo(AmmoType ammoType) //This is a public function which returns nothing and takes in the ammo type class. this is to reduce the current ammo amount in a certain gun
    {
       GetAmmoSlot(ammoType).ammoAmmount--; //the get ammo slot function is used taking in the ammo type and the ammo ammount in that ammo type is reduced by 1 
    }
    
    public void IncreaseAmmo(AmmoType ammoType, int ammoPickedUp) //This is a public function with no return type called increase ammo, it takes in 2 variables of the ammo type class and an int
        //of the ammo picked upj
    {
        GetAmmoSlot(ammoType).ammoAmmount += ammoPickedUp; //the get ammo slot function is played passing in the ammo type used and then increasing the ammo amount by the amount of ammo picked up
    }
    private AmmoSlot GetAmmoSlot(AmmoType ammoType) //the get ammo slot is the main primary function of the script and class. It returns an ammo slot class and passes in an ammo type class
    {
        foreach (AmmoSlot slot in ammoSlots) //a for each loop is played checking each ammo slot in the ammo slot index/list. Comparing and checking which are the same
        {
            if (slot.ammoType == ammoType) //if the ammo type is equivalent to the slot ammo type index currently held
            {
                return slot; //the slot is returned and given
            }
        }
        return null; //if nothing is found, it returns null and that there is no corresponding ammo type.
    }
}
