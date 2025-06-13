using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BreakerBox : Interactable
{
    public override bool SupportsPositiveInteraction => false;
    public AudioClip leverSound;
    public float karmaValue = 20;
    public List<AudioClip> switchClips;

    private Animator animator;
    private AudioSource audioSource;
       

    void Start()
    {
        animator = GetComponent<Animator>();
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

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        AudioSource.PlayClipAtPoint(leverSound, transform.position);
        PlaySwitchSounds();
        //AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
        animator.Play("turn_off");

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = karmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Turned off the breaker. Karma Type: " + GetKarmaType());
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

    public void PlaySwitchSounds()
    {
        StartCoroutine(PlaySoundsInOrder());
    }

    private IEnumerator PlaySoundsInOrder()
    {
        foreach (AudioClip clip in switchClips)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
