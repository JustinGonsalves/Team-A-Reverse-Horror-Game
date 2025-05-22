using UnityEngine;

public class KarmaManager : MonoBehaviour
{
    public int totalKarma = 50;

    public int goodActions = 0;
    public int badActions = 0;


    public void ApplyKarmaFromInteractable(int karmaValue, Interactable.KarmaType karmaType)
    {
        totalKarma += karmaValue;

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

    public void ApplyKarmaFromGurgle(int karmaValue)
    {
        totalKarma += karmaValue;
        badActions++;

        Debug.Log($"Karma changed by {karmaValue}. Total Karma: {totalKarma}");
    }
}
