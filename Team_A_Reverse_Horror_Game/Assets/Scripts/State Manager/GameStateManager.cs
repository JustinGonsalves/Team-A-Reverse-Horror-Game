using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public string sceneToLoad;
    public float cutsceneDelay = 5f;

    private float cutsceneTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        //Check if win state is active
        //If win state is achieve then play cutscene
        //LoadCutscene();
    }

    //Function to load a scene
    public void LoadScene()
    {
        //Access the scene manager to load a scene
        SceneManager.LoadScene(sceneToLoad);
    }

    //Function to load cutscene
    public void LoadCutscene()
    {
        //Count up the timer in seconds
        cutsceneTimer += Time.deltaTime;

        //If enough time has passed
        if (cutsceneTimer >= cutsceneDelay)
        {
            //Load the next scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    
//Quit game function
public void QuitGame()
    {
        //Allows game to exit application
        Application.Quit();
    }

}
