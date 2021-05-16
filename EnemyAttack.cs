using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerHealth target; //this is a public reference to the player health class and defined as target
    public float damage = 20f; //this is a public float for the amount of damage the enemy attack does which is instansiated at 20
    // Start is called before the first frame update
    void Awake()
    {
        target = FindObjectOfType<PlayerHealth>(); //the target variable then is assigned the value of the player health class, there is only one script active with a player health class, therefore
        //we can use the find object of type as only one will excist within the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackFrame()
    {
        if (target == null) //this is the function at the attack frame of the animation, where the attack connects and occurs. if the target hit is null, the script returns and nothing is played
            return;
        target.TakeDamage(damage); //else if the target is not null and the player health component is attached to the enemy, the take damage function is executed with the damage float passing in
    }
}
