using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum KarmaType { None, Good, Bad}
    
    protected KarmaType selectedKarmaType = KarmaType.None;
    protected float karmaValueEffect = 0f;

    public bool hasBeenInteracted = false;

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
        if (hasBeenInteracted) return;
        hasBeenInteracted = true;

        selectedKarmaType = KarmaType.Good;
        //Only add this if good/bad choice presented on chosen object
        karmaValueEffect = 15;

        Debug.Log("No custom interact function written");
     
    }

    // Fill this with custom object code
    public virtual void NegativeInteract()
    {
        if (hasBeenInteracted) return;
        hasBeenInteracted = true;

        selectedKarmaType = KarmaType.Good;
        //Only add this if good/bad choice presented on chosen object
        karmaValueEffect = -15;

        Debug.Log("No custom interact function written");
    }

    public virtual void OnHoverStart() { }

    public virtual void OnHoverEnd() { }
}
