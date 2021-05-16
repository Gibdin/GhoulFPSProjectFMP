using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public Camera fpsCam; //Assigning a reference to the main camera used for the game
    public float range = 100f; //The range of the gunshot bullet range
    public float damage = 25f; //The damage value of the weapon on this gun 
    public ParticleSystem muzzleFlash; //The muzzleflash attached to this weapon as a child in the gun 
    public GameObject hitEffect; //A reference to the hit effect which is instansized only in game time

    public float nextTimeToFire = 0f; //This is a way to adjust the firerate of a weapon so it does not fire off how long the button is being held for.
    public Ammo ammoSlot; //a referene to the ammo class into a variable called ammoSlot
    private bool canShoot = true; //a boolean check to ensure the player is able to shoot after firing, this is to create a fire rate system where the player has to wait for the next bullet rather than all the bullets 
    //firing constantly, burning through ammo quickly
    public AmmoType ammoType; //a reference to the AmmoType class labelled ammoType
    public TextMeshProUGUI ammoUI; //a reference to the text mesh pro text child of the canvas labelled as ammoUI

    // Start is called before the first frame update
    void Start()
    {
        ammoSlot = GetComponentInParent<Ammo>(); //the ammoslot is assigned to the script inside the parent of the game object sitting on the script which will be the player. As the player holds on to the weapon game
        //object inside its prefab which where it holds the weapon script for each weapon on indivually
        fpsCam = Camera.main; //Set the fps cam to the main camera used which is the first person camera of the game 
    }

    private void OnEnable()
    {
        canShoot = true; //This is a bug fix made to prevent being unable to shoot if the weapon is disabled from switching after firing and quickly swapping, without this, the gun will jam after firing and immediately
        //switching
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo(); //this function is constanlty executed every frame, its to update and maintain the ui of the ammo count in the bottom right of the game, this is necessary so the player knows how much ammo they have
        //at all times

        if (Input.GetButton("Fire1") && canShoot)// if the player presses the button Fire1 which is a reference to the left mouse button of a mouse and if the can shoot fire rate boolean is true 
        {
            StartCoroutine(Shoot()); //If the if statement returns true then a courtine begins which works on the side of other methods as the update method is played out.
        }

    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType); //This method takes the current ammo as its integer value and executed the get current ammo execution sending the ammo type to pass into the function
        ammoUI.text = currentAmmo.ToString(); //the text component of the text mesh pro is assigned the same value as the current ammo value which is changed to a string value
    }

    IEnumerator Shoot() //The IEnumerator is a syntax to call for a corutine
    {
        canShoot = false; //after the firing function is called, the first step is the can shoot variable turning to false so the firerate stays fixed after firing, giving a while till the next bullet is ready to shoot
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0) //if there is any ammo held inside the gun, the statement returns true and the following methods are executed 
        {
            PlayMuzzleFlash(); //When the shoot function is called, the PlayMuzzleFlash function is carried out aswell as the fire function being called 
            Fire(); //the fire function is also called 
            ammoSlot.ReduceCurrentAmmo(ammoType); //current ammo is reduced for everytime a shot is fired
        }
        yield return new WaitForSeconds(nextTimeToFire); //the time to fire is a value of seconds held in the wait for seconds which is how long it takes till the new shot is ready to fire
        canShoot = true; //the next shot ready to fire is confirmed by the can shoot boolean being true 
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play(); //when the function is called, the muzzleflash play function is called which is inbuilt in the particle 
        
    }

    private void Fire()
    {
        RaycastHit hit; //A raycast variable called hit is created when the fire function is called 
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) //This if statement creates a physics raycast inside a boolean statement which returns true if 
            //the raycast hits anything, it starts at the fps cams vector 3 transform position as origin and then the fps cams forward direction as a direction, the hit raycast is plugged into the 
            //if statement and stores all the details of the raycasts properties, the range is self explanatory as it shows the range of how far the raycast will go for
        {
            HitImpact(hit); //the hit impact function is called taking in the hit raycast as a value 
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>(); //a variable of type enemy heatlh class which is another script in unity is assigned of the hit of the raycast's transform
            // gameobject has the EnemyHealth component
            Debug.Log("I hit: " + hit.transform.name);
            if (target == null) return; //If the target variable returns as null or na it means no enemy has been hit and rather something else, the code will return to its original state
            target.TakeDamage(damage); //the target's take damage function is called taking in the damage float value 
            Debug.Log("I hit: " + hit.transform.name); //debug 
        }
        else
        {
            return; //if the raycast bool returns false the code returns
        }
    }


    private void HitImpact(RaycastHit hit) //This function is called witholding the raycast 
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //the impact variable of type gameobject is created which is a variable
        //impact instantiate the game object at the hit point for the vector 3 location and a rotation using the normal raycast's rotation
        Destroy(impact, 1); //The impact effect is destroyed after a 1 second delay 
    }
}
