using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public string sceneToLoad;
    
    //Function to load a scene
    public void LoadScene()
    {
        //Access the scene manager to load a scene
        SceneManager.LoadScene(sceneToLoad);

        //Locks the cursor so the player can take control of the character
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    //Quit game function
    public void QuitGame()
    {
        //Allows game to exit application
        Application.Quit();
    }

}
