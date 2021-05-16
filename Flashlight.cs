using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour //this is a script for the flashlight which is spot light light effect on the player facing forward.
{
    public float lightDecay = .25f; //this is the rate the light decays from the spot light
    public float angleDecay = 1f; //this is the rate the angle of the light decays on the spot light
    public float minimumAngleDecay = 30f; //I dont want the angle decay to become practically a small circle so I used a minimum angle so it stays fairly open but not too big nor small as minimum

    Light light; //a variable of the light class

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>(); //the variable is assigned the component on the game object of the light
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLightAngle(); //Every frame the decrease light angle and intensity method is called so the flashlight is constantly running out 
        DecreaseLightIntensity();
    }

    private void DecreaseLightIntensity() //this method takes the intensity of the light component and subtracts the light decay and assigns the value of the sum of the equation * time.delta time
        //so the intensity fades out at a fixed rate
    {
        light.intensity -= lightDecay * Time.deltaTime; 
    }

    private void DecreaseLightAngle() //this method checks with an if statement if the spot angle component of the light is less than or equal to minimum angle decay, if the statement returns true 
        //the spot angle is at the minimum angle and the if statement returns
    {
        if (light.spotAngle <= minimumAngleDecay)
        {
            return;
        }
        else
        {
            light.spotAngle -= angleDecay * Time.deltaTime; //if the statement returns false then the angle decay is subtracted on the spot angle and the spot angle is given the sum of the equation
            //it is multiplied by time.delta time so the spot angle works at its own fixed rate rather frame by frame 
        }
    }

    public void RestoreLightAngle(float restoreAngle) //this function is to restore the light angle when a battery is picked up in game. It takes in the float of the restore angle it recovers
    {
        light.spotAngle = restoreAngle; //the spot angle of the component is now the same value of the restore angle passed through
    }

    public void RestoreLightIntensity(float intensityAmount) //this function is the same as the restore light function however this changes the value of the intensity amount to the max again
    {
        light.intensity = intensityAmount;
    }
}
