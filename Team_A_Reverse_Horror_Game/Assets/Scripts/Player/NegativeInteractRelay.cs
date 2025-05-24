using UnityEngine;

public class NegativeInteractRelay : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    public void NegativeInteractTiming()
    {
        if (playerInteraction == null)
        {
            Debug.Log("Assign a reference to the playerInteraction script");
        }

        if (playerInteraction != null)
        {
            playerInteraction.pendingInteraction.NegativeInteract();
            playerInteraction.pendingInteraction = null;
        }
    }
}
