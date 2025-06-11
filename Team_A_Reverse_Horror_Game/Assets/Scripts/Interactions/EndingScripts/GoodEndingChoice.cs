using UnityEngine;

public class GoodEndingChoice : Interactable
{
    public override bool SupportsNegativeInteraction => false;

    public override void PositiveInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

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
