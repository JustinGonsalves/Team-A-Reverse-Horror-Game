using UnityEngine;

public class MusicBox : Interactable
{
    public float karmaValue = -20;


    // Empty reference to AudioSource component
    private AudioSource audioSource;
    // Empty reference to AnimatorController
    private Animator animator;

    // Disables negative interactions
    public override bool SupportsNegativeInteraction => false;

    private void Start()
    {
        // Assign reference to audioSource component
        audioSource = GetComponent<AudioSource>();
        // Assign reference to animator component
        animator = GetComponent<Animator>();
    }

    public override void OnStageActivated()
    {
        PlayMusic();
    }

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void PositiveInteract()
    {
        // Disable multiple interactions
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

        StopMusic();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = karmaValue;

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
        base.OnHoverStart();
    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }

    // Call this to start the music box
    public void PlayMusic()
    {
        if (audioSource != null)
        {
            // Starts playing the music
            audioSource.Play();
            // Enables handle rotation
            animator.Play("handle_turn");
        }
    }

    // Call this to end the music (used in PositiveInteract())
    public void StopMusic()
    {
        // Stop music
        audioSource.Stop();
        // Disable handle rotation
        animator.Play("stop");
    }
}
