using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 100f; //A float variable of the players max health 
    public float currentHealth; //Another empty variable assigned to take in the current health 
    public void TakeDamage(float damage) //This function is for the enemy to take damage from the playspace and external threats it takes in the float variable of damage from an external script 
    {
        GetComponent<EnemyAI>().DamageTaken(); //This acquires the component on the game component which holds the enemy ai script, the enemy ai scripts damage taken function is then executed
        //in order for any occured damage to pass through
        currentHealth -= damage; //Current health is reduced and then the resulted output by the damage value 
        if (currentHealth <= 0) //this statement checks to see if the current health of the enemy is less than or equal to 0 
        {
            Destroy(gameObject); //if the statement is true, the game object is destroyed 
        }
    }
    void Awake() 
    {
        currentHealth = maxHealth; //Once the scene starts, the enemies current health is assigned to the max health value.
    }
}
