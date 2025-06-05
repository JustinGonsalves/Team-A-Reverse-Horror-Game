using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    // Sets the message shown on screen
    public void ShowMessage(string message)
    {
        objectiveText.text = message;
        objectiveText.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        objectiveText.gameObject.SetActive(false);
    }
}
