using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //public GameObject messagePopup;
        // Reference to voiceline at stage start
        public AudioClip objectiveStartLine;
    }

    // A list of all the stages to be set in the inspector
    public List<Stage> stages;
    // Tracks the current stage, starts on 0
    private int currentStage = 0;

    // Karma-based final stages
    public KarmaManager karmaManager;
    public Stage goodFinalStage;
    public Stage badFinalStage;
    public Stage neutralFinalStage;
    private Stage currentFinalStage;

    private bool finalStageStarted = false;
    public AudioSource audioSource;

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
            //objectiveUI.ShowMessage("All objectives complete!");
            return;
        }

        // Grab the current stage from the list
        Stage current = stages[currentStage];

        // Play the voiceline and wait for it to finish before enabling interactables
        StartCoroutine(ShowStageAfterVoiceline(current));
    }

    private IEnumerator ShowStageAfterVoiceline(Stage current)
    {
        // Call OnStageActivated for all interactables
        foreach (var item in current.interactables)
        {
            item.OnStageActivated();
        }

        // Play the voiceline
        if (current.objectiveStartLine != null && audioSource != null)
        {
            audioSource.clip = current.objectiveStartLine;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // Enable interactables after voiceline
        foreach (var item in current.interactables)
        {
            item.canBeInteracted = true;
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

        if (currentStage < stages.Count)
        {
            ShowCurrentStage();
        }
        else if (!finalStageStarted)
        {
            StartCoroutine(WaitAndStartFinalStage());
        }

    }

    // Coroutine to adjust match animation trigger for final Karma update
    private IEnumerator WaitAndStartFinalStage()
    {
        yield return new WaitForSeconds(1.1f);
        StartFinalStage();
    }

    // Use tab to show the current objective
    private void ShowObjective()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (currentStage < stages.Count)
            {
                objectiveUI.ShowMessage(stages[currentStage].objectiveReminder);
            }
            else if (finalStageStarted && currentFinalStage != null)
            {
                objectiveUI.ShowMessage(currentFinalStage.objectiveReminder);
            }

        }
        else
        {
            objectiveUI.HideMessage();
        }
    }

    private void StartFinalStage()
    {
        finalStageStarted = true;

        Stage finalStageToUse;

        float karma = karmaManager.totalKarma;

        if (karma < 40)
        {
            finalStageToUse = goodFinalStage;
            Debug.Log("Assigning GOOD final objective.");
        }
        else if (karma > 60)
        {
            finalStageToUse = badFinalStage;
            Debug.Log("Assigning BAD final objective.");
        }
        else
        {
            finalStageToUse = neutralFinalStage;
            Debug.Log("Assigning NEUTRAL final objective (choice).");
        }

        currentFinalStage = finalStageToUse;
        StartCustomStage(finalStageToUse);
    }

    private void StartCustomStage(Stage stage)
    {
        if (stage.objectiveStartLine != null && audioSource != null)
        {
            audioSource.clip = stage.objectiveStartLine;
            audioSource.Play();
        }

        foreach (var item in stage.interactables)
        {
            item.canBeInteracted = true;
            item.OnInteracted += OnFinalStageInteracted;
        }
    }

    private void OnFinalStageInteracted(Interactable chosen)
    {
        foreach (var item in currentFinalStage.interactables)
        {
            item.OnInteracted -= OnFinalStageInteracted;
            item.canBeInteracted = false;
            if (item != chosen)
            {
                item.DisableInteraction();
            }
        }

        Debug.Log("Final stage completed. Triggering ending.");
        TriggerEnding(chosen);
    }

private void TriggerEnding(Interactable chosen)
    {
        if (chosen.CompareTag("GoodEnding"))
        {
            SceneManager.LoadScene("GoodEndingScene");
        }
        else if (chosen.CompareTag("BadEnding"))
        {
            SceneManager.LoadScene("BadEndingScene");
        }
        else
        {
            Debug.LogWarning("Interactable is not tagged with a valid ending tag.");
        }
    }

}
