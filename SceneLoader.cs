using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //The scenemangement toolset is used to access the scene managing systems

public class SceneLoader : MonoBehaviour
{

    public void ReloadGame() //this is a function to reload the game once the game is over if the player dies. when the reload game function is execute in the ui, the following instructions
        //are carried out 
    {
        SceneManager.LoadScene(0); //the scenemanager loads the first scene again which is the first and only level
        Time.timeScale = 1; //the timescale of the scene and game is reset back to its original state as once the player dies the time scale is at 0 to freeze the game. The original timescale
        //Is now set back to normal game speed
    }

    public void QuitGame() //The quit game function is executed at the button press, this immediately exits the game when the game is built and does nothing in the scene
    {
        Application.Quit();
    }
}
