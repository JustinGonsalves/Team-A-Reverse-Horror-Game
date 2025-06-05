using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum KarmaType { None, Good, Bad}
    
    protected KarmaType selectedKarmaType = KarmaType.None;
    protected float karmaValueEffect = 0f;

    // Prevents reuse
    public bool hasBeenInteracted = false;
    // Controlled by objective manager
    public bool canBeInteracted = false;

    // Event for when this object is used
    public Action<Interactable> OnInteracted;

    // By default both positive and negative interaction possible, override either in script if needed
    public virtual bool SupportsPositiveInteraction => true;
    public virtual bool SupportsNegativeInteraction => true;

    // In object script, can use ' return base.GetKarmaType(); '
    public virtual KarmaType GetKarmaType()
    {
        return selectedKarmaType;
    }

    // In object script, can use ' return base.GetKarmaValue(); '
    public virtual float GetKarmaValue()
    {
        return karmaValueEffect;
    }

    // Fill this with custom object code
    public virtual void PositiveInteract()
    {
        if (!canBeInteracted || hasBeenInteracted) return;

        hasBeenInteracted = true;
        // Tells the listener in the objective manager that this has been used
        OnInteracted?.Invoke(this); 

    }

    // Fill this with custom object code
    public virtual void NegativeInteract()
    {
        if (!canBeInteracted || hasBeenInteracted) return;

        hasBeenInteracted = true;
        // Tells the listener in the objective manager that this has been used
        OnInteracted?.Invoke(this);
    }

    public virtual void OnHoverStart()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null && !hasBeenInteracted && canBeInteracted)
        {
            outline.gameObject.SetActive(true);
        }
    }

    public virtual void OnHoverEnd()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }
    }

    // Used by the objective manager to disable interactions not chosen
    public virtual void DisableInteraction()
    {
        hasBeenInteracted = true;
    }
}
