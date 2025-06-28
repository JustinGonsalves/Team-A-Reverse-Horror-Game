using StarterAssets;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject controlMenu;
    public GameObject exitMenu;

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

        //Release the mouse so it can interact with the menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

        //Diable the control menu object 
        controlMenu.SetActive(false);

        //Diable the exit menu object 
        exitMenu.SetActive(false);

        //Find and reactivate the player controller
        FindAnyObjectByType<FirstPersonController>().enabled = true;

        //Locks the cursor so the player can take control of the character
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
