using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    // Reference to UI script
    public ObjectiveUI objectiveUI;
    
    // Defines what a stage looks like
    [System.Serializable]
    public class Stage
    {
        // Each stage contains a list of 1 or 2 interactables
        public List<Interactable> interactables;
        //Custom message to be shown when TAB is held
        public string objectiveReminder;
        // Reference to pop-up message on stage progression
        public GameObject messagePopup;
    }

    // A list of all the stages to be set in the inspector
    public List<Stage> stages;
    // Tracks the current stage, starts on 0
    private int currentStage = 0;

    private void Start()
    {
        ShowCurrentStage();
    }

    private void Update()
    {
        ShowObjective();
    }

    // Activates everything needed for the current stage
    private void ShowCurrentStage()
    {
        // If all the stages are complete, end the sequence
        if (currentStage >= stages.Count)
        {
            objectiveUI.ShowMessage("All objectives complete!");
            return;
        }

        // Grab the current stage from the list
        Stage current = stages[currentStage];

        // Make the pop-up message visible
        if (current.messagePopup != null)
        {
            current.messagePopup.SetActive(true);
        }

        // Go through each interactable object in the current stage
        foreach (var item in current.interactables)
        {
            // Allow the objects to be used
            item.canBeInteracted = true;
            // Start listening for when the object is used, when it is, call OnPlayerInteracted method
            item.OnInteracted += OnPlayerInteracted;
        }

        Debug.Log("Objective Stage " + (currentStage + 1) + " started");
    }
    
    // Called when the player interacts with an object from the current stage
    private void OnPlayerInteracted (Interactable chosen)
    {
        // Get a list of all interactables for the current stage
        List<Interactable> current = stages[currentStage].interactables;

        // Go through each of them
        foreach (var item in current)
        {
            // Stop listening for the objects in the current stage
            item.OnInteracted -= OnPlayerInteracted;
            // Prevents further interaction
            item.canBeInteracted = false;

            if (item != chosen)
            {
                // Disable the unused choice
                item.DisableInteraction();
            }
        }

        // Move to next round
        currentStage++;
        // Begin next set of interactables
        ShowCurrentStage();
        
    }

    // Use tab to show the current objective
    private void ShowObjective()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            objectiveUI.ShowMessage(stages[currentStage].objectiveReminder);
        }
        else
        {
            objectiveUI.HideMessage();  
        }
    }

}
