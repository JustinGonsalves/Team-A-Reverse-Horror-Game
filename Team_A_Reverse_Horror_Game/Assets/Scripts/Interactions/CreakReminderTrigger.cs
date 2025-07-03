using UnityEngine;

public class CreakReminderTrigger : MonoBehaviour
{
    public AudioClip voiceLine;
    public AudioSource playerAudioSource;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (hasBeenTriggered == false)
            {
                playerAudioSource.PlayOneShot(voiceLine);
                hasBeenTriggered = true;
            }
        }
    }
}
