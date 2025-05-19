using Unity.VisualScripting;
using UnityEngine;

public class BedroomLight : Interactable
{
    public GameObject auraObject;
    public int KarmaValue = 15;

    private bool isTurnedOn = true;

    public override KarmaType GetKarmaType()
    {
        return KarmaType.Good;
    }

    public override int GetKarmaValue()
    {
        return KarmaValue;
    }

    public override void Interact()
    {
        if (hasBeenInteracted) return;

        isTurnedOn = !isTurnedOn;
        Debug.Log(isTurnedOn ? "Light turned on" : "Light turned off");
        hasBeenInteracted = true;
        SetAuraActive(false);
    }

    public override void OnHoverStart()
    {
        if (!HasBeenInteracted)
        {
            SetAuraActive(true);
        }
    }

    public override void OnHoverEnd()
    {
        SetAuraActive(false);
    }


    private void SetAuraActive(bool active)
    {
        if (auraObject != null)
        {
            auraObject.SetActive(active);
        }
    }
}
