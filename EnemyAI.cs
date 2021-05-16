using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    public Transform player; //declaring a variable of the transform (location of the player in the playspace)
    public float alertRadius = 9f; //value assigned to the radius. The player entering this raidus will trigger the player
    public float turnSpeed = 4f; //a variable for the speed the enemy will turn at when going to face the player

    NavMeshAgent navMeshAgent; //assigning a variable to reference the game objects navmesh component 
    public float distanceFromPlayer = Mathf.Infinity; //this is a variable to estimate the distance of the enemy from the player. Its assigned with the value of infinity as setting it with no value or 0
    //has caused bugs and issues 
    public bool aggro = false; //a true or false statement which is a check for the state the enemy is in
    public Animator anim; //A reference to the animator component on the enemy 
    public float speed; //the speed the enemy ai moves at
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //the anim varaible is assigned the animator component on the same object the enemy ai script is on
        navMeshAgent = GetComponent<NavMeshAgent>(); //acquires the navmeshagent component on the same game object the enemy ai script is on which is the enemy 
        player = GameObject.Find("Player").GetComponent<Transform>(); //attach the player variable to the players transform, I search for the game object of the player which there is only one of
        //and then get the transform component of the player
        navMeshAgent.stoppingDistance = 1.9f; //assign the stopping distance of the navmesh agent 
        navMeshAgent.speed = speed; //assign the speed of the nav mesh agent to the same speed the speed value has

    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position); //This variable is given a value of the distance in a vector 3 value, this is assigned with the position of the
        //player and the position of the gameobject this script is attached too
        if (aggro) //an if statement to check if the aggro boolean is true 
        {
            Engage(); //if the statement is true the engage function is called 
        }
        else if (distanceFromPlayer <= alertRadius) //this else if checks if the distance from the player variable is less than or equal too the alert radius' value
        {
            aggro = true; //the else if is too check if aggro should be declared as true 
        }
    }

    private void Engage()
    {
        FaceTarget(); //this is a method 
        if (distanceFromPlayer >= navMeshAgent.stoppingDistance) //if the distance from the player is more than or equal to the navmesh agents component: stopping distance 
        {
            Chase(); //if the statement is true the chase function is called 
        }
        
        if (distanceFromPlayer < navMeshAgent.stoppingDistance) //if the distance from the player is less than or equal to the navemesh agents component: stopping distance
        {
            Attack(); //if the statement is true the attack function is called
        }
    }

    private void Attack() 
    {
        anim.SetBool("Attacking", true); //when the attack function is executed, the attacking bool is set to true so the animation begins, there is a frame in the animation where the attack 
        //connects to the target and the damage function is played through the enemy attack script.

    }

    public void DamageTaken() //this is a function incase damage has ben dealt before the target has been aggro'd the enemy is aggrod automatically and starts chasing even if the player is not in
        //threat range
    {
        aggro = true;

    }
    private void Chase() //this is a function for when the enemy begins to chase the target which is the player. 
    {
        anim.SetTrigger("Moving"); //the trigger for the moving animation is set to true as the player keeps moving during the chase. the animation is a sign of being chased.
        anim.SetBool("Attacking", false); //The boolean for the animation is set to false as the enemy starts to move again to ensure the move animation plays without the attack animation 
        //interrupting 
        navMeshAgent.SetDestination(player.position); //the chase function sets the destination of the game objects navmesh component to the position of the player. Chasing them.
    }

    private void FaceTarget() //this is a function to have the enemy rotate around when the enemy is close enough to the player and begins attacking, without this method the enemy will begin
        //attacking without facing the players direction, making it look awkward and that the enemy is not attacking the player
    {
        Vector3 direction = (player.position - transform.position).normalized;  //a vector 3 named direction is created which is equal to the subtraction of the enemy position from the position of
        //the player the .normalized function rounds up the sum to a magnitude of 1 
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); //a Quartenion is a rotation type, a variable is created for the rotation necessary for the enemy
        //to face the player. The variable is a look rotation method assigned a new vector 3, which takes in the direction on the x axis, 0 on the y and the direction on the z 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); //the rotation is then assigned the value of a Slerp. Slerp is a spherical rotation
        //from point a to point b at a fixed rate, point a is the transform original rotation of the player, the look rotation is the destination and the time.delta time * turnspeed is the speed
    }

    private void OnDrawGizmosSelected() //a debug function to identify variables in code as wireframes in game
    {
        Gizmos.color = Color.black; //the gizmo is colored black to highlight it in the grey background
        Gizmos.DrawWireSphere(transform.position, alertRadius); //The wiresphere is drawn at the starting point of the transform of the game object then has the radius of alertRadius
    }
}
