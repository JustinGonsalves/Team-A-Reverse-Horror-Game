using System.Collections;
using UnityEngine;

public class CreakReminderTrigger : MonoBehaviour
{
    public AudioClip voiceLine;
    public AudioSource playerAudioSource;
    public GameObject reminderPanel;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (hasBeenTriggered == false)
            {
                playerAudioSource.PlayOneShot(voiceLine);
                hasBeenTriggered = true;
                StartCoroutine(ShowText());
            }
        }
    }

    
    private IEnumerator ShowText()
    {
        reminderPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        reminderPanel.SetActive(false);
    }
}
