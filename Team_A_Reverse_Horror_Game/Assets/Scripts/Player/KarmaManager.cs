using UnityEngine;

public class KarmaManager : MonoBehaviour
{
    public float totalKarma = 50;

    public int goodActions = 0;
    public int badActions = 0;

    public KarmaBar karmaBar;


    public void ApplyKarmaFromInteractable(float karmaValue, Interactable.KarmaType karmaType)
    {
        // Add value of interaction
        totalKarma += karmaValue;

        // Log if Interaction is Good or Bad
        if (karmaType == Interactable.KarmaType.Good)
        {
            goodActions++;
        }

        if (karmaType == Interactable.KarmaType.Bad)
        {
            badActions++;
        }

        Debug.Log($"Karma changed by {karmaValue}. Total Karma: {totalKarma}. You've performed {goodActions} Good deeds, and {badActions} Bad deeds");
    }
}
