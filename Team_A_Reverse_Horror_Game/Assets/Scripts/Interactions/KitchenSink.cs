using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KitchenSink : Interactable
{
    public override bool SupportsNegativeInteraction => false;

    public float karmaValue = -20f;
    public ParticleSystem drip;
    public AudioClip closeDrawers;
    public Vector3 tapPosition;

    private AudioSource audioSource;
    private Animator animator;
    private bool isDripping = false;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        drip.Stop();
    }

    private void Update()
    {
        if (canBeInteracted && !isDripping)
        {
            StartDripping();
        }
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

        StopDripAndCloseCupboard();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = karmaValue;

        // Find Outline child game object
        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            // Disable once interacted
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Turned off sink and tidied up. Karma Type: " + GetKarmaType());
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

    private void StartDripping()
    {
        isDripping = true;
        drip.Play();
        audioSource.Play();
    }

    private void StopDripAndCloseCupboard()
    {
        drip.Stop();
        animator.Play("turn_off");
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(closeDrawers, tapPosition);
    }

}
