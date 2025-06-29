using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines.ExtrusionShapes;

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

    //Function to load a scene with a delay
    public void LoadSceneWithDelay()
    {   
        //Call delay coroutine to start a timer and load scene
        StartCoroutine(Delay());        
    }

    //Quit game function
    public void QuitGame()
    {
        //Allows game to exit application
        Application.Quit();
    }

    //Coroutine to function scene for a few seconds
    public IEnumerator Delay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(3f);

        //Access the scene manager to load a scene
        SceneManager.LoadScene(sceneToLoad);

        //Locks the cursor so the player can take control of the character
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
