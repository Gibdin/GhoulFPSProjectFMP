using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandle : MonoBehaviour //this class/script handles what happens after death.
{
    public Canvas gameOverCanvas; //a reference to the ui canvas enabled when the player dies in game. 


    void Start()
    {
        gameOverCanvas.enabled = false; //At the start when the unity scene plays, the canvas is disabled

    }

    public void Death() //the death function can be called from other scripts to call that the player is dead, when the player dies. the following scripts occurs
    {
        gameOverCanvas.enabled = true; //the game over canvas is enabeld
        Time.timeScale = 0; //the time scale of the game is set to 0 so it seems that the game has been frozen 
        FindObjectOfType<WeaponMaster>().enabled = false; //the weaponmaster script in the game is set to false so the player is unable to change weapon after death 
        Cursor.lockState = CursorLockMode.None; //the lock state of the cursor is unlocked so the player can interact with the ai
        Cursor.visible = true; //the visibility of the cursor is available again.
    }
}
