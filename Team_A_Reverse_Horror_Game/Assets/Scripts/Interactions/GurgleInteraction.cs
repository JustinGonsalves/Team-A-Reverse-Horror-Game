using UnityEngine;

public class GurgleInteraction : Interactable
{
    public override bool SupportsPositiveInteraction => false;

    public GameObject player;
    public AudioClip gurgleNoise;
    public float karmaValue = 20;
    private AudioSource playersAudioSource;

    private void Start()
    {
        playersAudioSource = player.GetComponent<AudioSource>();
    }

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        playersAudioSource.PlayOneShot(gurgleNoise);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = karmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);

        Debug.Log("Gurgled at child. Karma Type: " + GetKarmaType());
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

}
