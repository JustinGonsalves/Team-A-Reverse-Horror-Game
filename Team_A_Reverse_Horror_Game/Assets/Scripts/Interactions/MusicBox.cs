using UnityEngine;

public class MusicBox : Interactable
{
    // Empty reference to AudioSource component
    private AudioSource audioSource;
    // Empty reference to AnimatorController
    private Animator animator;
    // Reference for if music is playing or not
    private bool isTurnedOn = false;

    // Disables negative interactions
    public override bool SupportsNegativeInteraction => false;

    private void Start()
    {
        // Assign reference to audioSource component
        audioSource = GetComponent<AudioSource>();
        // Assign reference to animator component
        animator = GetComponent<Animator>();
        // Start disabled by default, will be enabled when music plays
        animator.enabled = false;
    }
    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the box isn't already playing and hasn't been interacted with already
        if (!isTurnedOn && !hasBeenInteracted)
        {
            // If the collider has the Player tag
            if (other.gameObject.CompareTag("Player"))
            {
                // Play music
                PlayMusic();
            }
        }
    }
    public override void PositiveInteract()
    {
        // Disable multiple interactions
        if (hasBeenInteracted) return;

        // Mark as interacted
        hasBeenInteracted = true;

        StopMusic();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = -20f;

        // Find Outline child game object
        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            // Disable once interacted
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Turned off music box. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());
    }

    public override void OnHoverStart()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null && !hasBeenInteracted)
        {
            outline.gameObject.SetActive(true);
        }
    }

    public override void OnHoverEnd()
    {
        Transform outline = transform.Find("Outline");
        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }
    }

    // Call this to start the music box
    public void PlayMusic()
    {
        if (audioSource != null)
        {
            // Log turn on
            isTurnedOn = true;
            // Starts playing the music
            audioSource.Play();
            // Enables handle rotation
            animator.enabled = true;
        }
    }

    // Call this to end the music (used in PositiveInteract())
    public void StopMusic()
    {
        // Log turn off
        isTurnedOn = false;
        // Stop music
        audioSource.Stop();
        // Disable handle rotation
        animator.enabled = false;
    }
}
