using UnityEngine;

public class TestLight : Interactable
{
    // Reference to light child object, assigned in editor
    public Light pointLight;

    // Light starts off
    private bool isTurnedOn = false;

    // Specify no negative interaction possible
    public override bool SupportsNegativeInteraction => false;

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;
        
        // Toggles the light on/off variable
        isTurnedOn = !isTurnedOn;
        hasBeenInteracted = true;

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = -16.6f;

        if (pointLight != null)
        {
            // Turns the light on
            pointLight.enabled = isTurnedOn;
            Transform outline = transform.Find("Outline");

            if (outline != null)
            {
                outline.gameObject.SetActive(false);

            }
        }

        Debug.Log("Interacted with LightSwitch. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());

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
