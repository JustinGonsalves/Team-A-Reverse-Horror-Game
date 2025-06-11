using UnityEngine;

public class BadEndingChoice : Interactable
{
    public override bool SupportsPositiveInteraction => false;

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        // Find Outline child game object
        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            // Disable once interacted
            outline.gameObject.SetActive(false);
        }
    }

    public override void OnHoverStart()
    {
        base.OnHoverStart();
    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }
}
