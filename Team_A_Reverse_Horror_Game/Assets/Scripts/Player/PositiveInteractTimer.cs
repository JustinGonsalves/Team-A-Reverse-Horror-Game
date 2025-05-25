using UnityEngine;

public class PositiveInteractTimer : MonoBehaviour
{
    // This timer script allows an animation event to trigger the interact function

    // Reference to player interaction script
    public PlayerInteraction playerInteraction;

    public void PositiveInteractTiming()
    {
        // If reference not assigned
        if (playerInteraction == null)
        {
            Debug.Log("Assign a reference to the playerInteraction script");
        }

        // If reference assigned properly
        if (playerInteraction != null)
        {
            // Triger the positive interact function of current hovered interactable
            playerInteraction.pendingInteraction.PositiveInteract();
            // Clear reference
            playerInteraction.pendingInteraction = null;
        }
    }
}
