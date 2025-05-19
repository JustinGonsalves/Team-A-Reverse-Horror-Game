using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum KarmaType { None, Good, Bad}

    protected bool hasBeenInteracted = false;
    public bool HasBeenInteracted => hasBeenInteracted;

    public virtual KarmaType GetKarmaType()
    {
        return KarmaType.None;
    }

    public virtual int GetKarmaValue()
    {
        return 0;
    }

    public virtual void Interact()
    {
        if (hasBeenInteracted) return;
        Debug.Log("No Interact function");
        hasBeenInteracted = true;        
    }

    public virtual void OnHoverStart() { }

    public virtual void OnHoverEnd() { }
}
