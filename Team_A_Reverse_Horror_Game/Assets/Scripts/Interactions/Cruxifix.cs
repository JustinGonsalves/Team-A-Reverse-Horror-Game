using UnityEngine;

public class Cruxifix : Interactable
{
    public override bool SupportsPositiveInteraction => false;

    public float karmaValue = 20f;
    public AudioClip crossFall;

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

        animator.Play("fall");
        //AudioSource.PlayClipAtPoint(crossFall, transform.position);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = karmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Flipped the crucifix. Karma Type: " + GetKarmaType());
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
