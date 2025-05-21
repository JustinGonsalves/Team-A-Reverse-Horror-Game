using UnityEngine;

public class TestLight : Interactable
{
    public Light pointLight;

    private bool isTurnedOn = false;
  
    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;
        
        Transform outline = transform.Find("Outline");
        isTurnedOn = !isTurnedOn;
        hasBeenInteracted = true;

        if (pointLight != null)
        {
            pointLight.enabled = isTurnedOn;
            outline.gameObject.SetActive(false);
        }

    }

    public override void NegativeInteract()
    {
        Object.FindFirstObjectByType<PlayerInteraction>().ClearHoveredReference();
        Destroy(gameObject);
        hasBeenInteracted = true;
    }

    public override void OnHoverStart()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null && !hasBeenInteracted)
        {
            outline.gameObject.SetActive(true);
        }
    }

    public override void OnHoverEnd()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }
    }
}
