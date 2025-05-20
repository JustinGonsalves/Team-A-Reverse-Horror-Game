using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum KarmaType { None, Good, Bad}

    //protected bool hasBeenInteracted = false;
    //public bool HasBeenInteracted => hasBeenInteracted;
    public bool hasBeenInteracted = false;

    public virtual KarmaType GetKarmaType()
    {
        return KarmaType.None;
    }

    public virtual int GetKarmaValue()
    {
        return 0;
    }

    public virtual void PositiveInteract()
    {
        //if (hasBeenInteracted) return;
        //hasBeenInteracted = true;
        Debug.Log("No Interact function");
     
    }

    public virtual void NegativeInteract()
    {
        //if (hasBeenInteracted) return;
        //hasBeenInteracted = true;
        Debug.Log("No Interact function");
    }

    public virtual void OnHoverStart() { }

    public virtual void OnHoverEnd() { }
}
