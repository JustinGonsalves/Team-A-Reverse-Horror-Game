using UnityEngine;

public class TestLight : Interactable
{
    public Light pointLight;
    private bool isTurnedOn = false;

    // Specify no negative interaction possible
    public override bool SupportsNegativeInteraction => false;

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override int GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;
        
        isTurnedOn = !isTurnedOn;
        hasBeenInteracted = true;

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = 15;

        if (pointLight != null)
        {
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
