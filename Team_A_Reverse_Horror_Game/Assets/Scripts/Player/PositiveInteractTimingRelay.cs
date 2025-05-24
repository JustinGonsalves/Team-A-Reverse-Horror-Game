using UnityEngine;

public class PositiveInteractTimingRelay : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    public void PositiveInteractTiming()
    {
        if (playerInteraction == null)
        {
            Debug.Log("Assign a reference to the playerInteraction script");
        }

        if (playerInteraction != null)
        {
            playerInteraction.pendingInteraction.PositiveInteract();
            playerInteraction.pendingInteraction = null;
        }
    }
}
