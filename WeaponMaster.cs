using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMaster : MonoBehaviour //This script sits on the parent object of all the weapons and sets what weapon is active.
{
    public int currentWeapon = 0; //This is a int of the current weapon to track what weapon is currently held, it starts at 0 as the first weapon 

    // Start is called before the first frame update
    void Start()
    {
        SetWeaponActive(); //at the start the set weapon active function is called to set the weapon to the first in the list of weapons according to the order ascending in numbers as it goes down 
    }

    void Update() //the update method is called once every frame 
    {
        int previousWeapon = currentWeapon;  //a new variable is created which takes in the previous weapon and is given the same value as the weapon currently held

        KeyInputProcess(); //This is a function to proceess any changes to the weapon choice on the number row

        if (previousWeapon != currentWeapon) //If the frame the weapon is changed and the previous weapon no longer equals to same index as the current weapon
        {
            SetWeaponActive(); //the set weapon active method is called and executed
        }
    }

    private void KeyInputProcess() //this method is to only process the input of keys to change the current weapon
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //alpha 1 is the 0th index and +1 for each other number, if the input is pushed down the current weapon index is changed
        {
            currentWeapon = 0;
        }
        if  (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void SetWeaponActive() //this method is to activate a certain game object/weapon in this case and disables any other weapon 
    {
        int weaponIndex = 0; //an int variable called weapon index is created at the 0th value

        foreach (Transform weapon in transform) //a for each loop is created of a transform type labelled as weapons in the list of transform is created
        {
            if (weaponIndex == currentWeapon) //for each index in the transform, if the weapon index is the current weapon the game object is set to active. if it isn't then its set to false and 
                //the weapon index is incremented, until the weapon index is the same as the index of the current weapon.
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
