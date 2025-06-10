using UnityEngine;

public class BreakerBox : Interactable
{
    public override bool SupportsPositiveInteraction => false;
    public AudioClip leverSound;
    public AudioClip powerDownSound;
    public float karmaValue = 20;

    private Animator animator;
       

    void Start()
    {
        animator = GetComponent<Animator>();    
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

        //AudioSource.PlayClipAtPoint(leverSound, transform.position);
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
}
