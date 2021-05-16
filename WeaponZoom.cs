using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour //this is a script to for specific weapons to zoom further in while holding down the mouse 2 button
{
    public Camera cam; //a reference to the first person camera 
    public float zoomedOut = 90f; //The zoomed out field of view as a float value
    public float zoomedIn = 45f; //the zoomed in field of view  as a float value
    public CameraControl camControl; //a variable of the camera control class as a reference to the camera control on the main camera
    public float sensZoomedIn = 100f; //the sensitivity of the mouse while zoomed in as a float value
    public float sensZoomedOut = 200f; //the sensitivity of the mouse while zoomed out as a float value
    private void Awake()
    {
        cam = Camera.main; //the cam variable is assigned the main camera which is the only camera in the game
        camControl = cam.GetComponent<CameraControl>(); //the cam control variable is assigned the component of the camera control script sitting on the main and only camera
    }

    public void OnDisable() //When the object is disabled this method will activate, its to prevent staying zoomed in in other weapons if the weapon changes
    {
        cam.fieldOfView = zoomedOut; //the field of view of the camera is set to zoomed out value 
        camControl.mouseSens = sensZoomedOut; //the mouse sens is set to the sens when the camera is zoomed out
    }

    private void Update() //this method is called once every frame
    {
        if (Input.GetMouseButton(1)) //if the mouse button is held down on the frame, the statement returns true and the zoom in function is executed
        {
            ZoomIn();

        }
        if (Input.GetMouseButtonUp(1)) //if the mouse button is released on the frame, the statement returns true and the zoom out function is executed 
        {
            ZoomOut(); 
        }
    }

    private void ZoomOut() //when this function is executed, the field of view of the camera is assigned the same value as the zoomed out value and the cam control mouse sens is set the same
        //value as the sens zoomed out value 
    {
        cam.fieldOfView = zoomedOut;
        camControl.mouseSens = sensZoomedOut;
    }

    private void ZoomIn() //when this function is executed the field of view of the camera is assigned the same value as the zoomed in value and the cam control mosue sens is set the same 
        //value as the sens zoomed in value
    {
        cam.fieldOfView = zoomedIn;
        camControl.mouseSens = sensZoomedIn;
    }
}
