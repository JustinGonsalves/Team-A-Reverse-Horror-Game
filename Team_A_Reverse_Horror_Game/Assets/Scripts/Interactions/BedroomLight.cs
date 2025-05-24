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

    public override float GetKarmaValue()
    {
        return KarmaValue;
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;
        hasBeenInteracted = true;

        isTurnedOn = !isTurnedOn;
        Debug.Log(isTurnedOn ? "Light turned on" : "Light turned off");
        SetAuraActive(false);
    }

    public override void OnHoverStart()
    {
        if (!hasBeenInteracted)
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
