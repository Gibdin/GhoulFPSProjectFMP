using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour //This script is specifically for the player health value 
{
    public float maxHealth = 100; //here we instantiate the variable max health and assign the value of 100 
    public float currentHealth; //we assign another variable called current health without a value as it will change and be assigned in real time during gameplay
    public DeathHandle deathHandle; //This is a variable of a death handle class, this is so we can call the class inside another script in the game and access its functions to commmunicate between scripts
    public TextMeshProUGUI healthDisplay; //This is a reference to the tmp text object inside the canvas in the ui 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; //The current health value is assigned the same value as the max health at the start of the game.
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth(); //this function is called every frame as its in the update method
    }

    private void DisplayHealth()
    {
        healthDisplay.text = currentHealth.ToString(); // in this method the text component of TMP is called to be assigned the same value as whatever the current health value is, since current health value is of float
        //type, it can not be directly converted without any difference, therefore the to string method converts the float to string.
    }

    public void TakeDamage(float damage) //This function is for the player to take damage from the playspace and external threats it takes in the float variable of damage from an external script 
    {
        currentHealth -= damage; //Current health is reduced and then the resulted output by the damage value 
        if (currentHealth <= 0) //if the current health is less than or equal to 0 
        {
            deathHandle.Death(); //The death handle class is called and the death method is executed when the player dies.
        }
    }
}
