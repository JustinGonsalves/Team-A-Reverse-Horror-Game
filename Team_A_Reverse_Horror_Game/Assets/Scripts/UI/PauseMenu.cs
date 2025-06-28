using StarterAssets;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        //If escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //and if the game is paused
            if (isPaused)
            {
                //Run the Resume Game function
                ResumeGame();
            }
            else
            {
                //Else run the Pause Game function
                PauseGame();
            }
        }        
    }

    //Function to Pause Game
    public void PauseGame()
    {
        //Pause the time of the game to freeze objects and interactions
        Time.timeScale = 0;

        //Set the paused game to true
        isPaused = true;

        //Set the pause menu object to active
        pauseMenu.SetActive(true);

        //Find the player controller and disable it
        FindAnyObjectByType<FirstPersonController>().enabled = false;
    }

    //Function to Resume Game
    public void ResumeGame()
    {
        //Resume the time of the game to unfreeze objects and interactions
        Time.timeScale = 1;

        //Set the pause game to false
        isPaused = false;

        //Diable the pause menu object 
        pauseMenu.SetActive(false);

        //Find and reactivate the player controller
        FindAnyObjectByType<FirstPersonController>().enabled = true;
    }


}
