using UnityEngine;

public class Curtain : Interactable
{
    public override bool SupportsNegativeInteraction => false;

    public GameObject gurgleObject;
    public AudioClip openSound;
    public float karmaValue = -20f;

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

    public override void PositiveInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

        AudioSource.PlayClipAtPoint(openSound, transform.position);
        animator.Play("open");
        gurgleObject.SetActive(false);

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = karmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Closed the curtains. Karma Type: " + GetKarmaType());
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
