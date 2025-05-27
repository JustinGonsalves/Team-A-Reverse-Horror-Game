using UnityEngine;

public class NegativeInteractTimer : MonoBehaviour
{
    // This timer script allows an animation event to trigger the interact function
    
    // Reference to player interaction script
    public PlayerInteraction playerInteraction;
    // Reference to Karma Manager script;
    public KarmaManager karmaManager;

    public void NegativeInteractTiming()
    {
        // If reference not assigned
        if (playerInteraction == null)
        {
            Debug.Log("Assign a reference to the playerInteraction script or KarmaManager");
            return;
        }

        // If reference assigned properly
        if (playerInteraction != null)
        {
            // Triger the negative interact function of current hovered interactable
            playerInteraction.pendingInteraction.NegativeInteract();
            //Apply Karma Values
            karmaManager.ApplyKarmaFromInteractable(playerInteraction.pendingInteraction.GetKarmaValue(),playerInteraction.pendingInteraction.GetKarmaType());
            // Clear reference
            playerInteraction.pendingInteraction = null;
        }
    }
}
