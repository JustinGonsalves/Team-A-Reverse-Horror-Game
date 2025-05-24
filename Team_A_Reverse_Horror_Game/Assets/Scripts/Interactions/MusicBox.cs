using UnityEngine;

public class MusicBox : Interactable
{
    private AudioSource audioSource;
    public override bool SupportsNegativeInteraction => false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (hasBeenInteracted) return;

        hasBeenInteracted = true;

        audioSource.Stop();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = -16.6f;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
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

    public void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
