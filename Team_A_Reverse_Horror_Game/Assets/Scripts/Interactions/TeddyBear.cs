using UnityEngine;

public class TeddyBear : Interactable
{
    public AudioClip hitNoise;
    public AudioClip goodVoiceline;
    public AudioClip badVoiceline;
    public float pushForce = 3f;
    public float positiveKarmaValue = -20f;
    public float negativeKarmaValue = 20f;
    public Material teddyEyes; // Assign the base eye material in the Inspector

    private AudioSource audioSource;
    private Renderer[] eyesRenderers = new Renderer[2];
    private Material[] eyesMaterialInstances = new Material[2];
    private Color originalEyeColor;

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Find both eye child objects
        string[] eyeNames = { "LeftEye", "RightEye" };
        for (int i = 0; i < eyeNames.Length; i++)
        {
            Transform eyeTransform = transform.Find(eyeNames[i]);
            if (eyeTransform != null)
            {
                eyesRenderers[i] = eyeTransform.GetComponent<Renderer>();
                if (eyesRenderers[i] != null && teddyEyes != null)
                {
                    eyesMaterialInstances[i] = new Material(teddyEyes);
                    eyesRenderers[i].material = eyesMaterialInstances[i];
                }
            }
        }

        // Store the original color from the base material
        if (teddyEyes != null)
            originalEyeColor = teddyEyes.color;
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

        audioSource.PlayOneShot(goodVoiceline);

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = positiveKarmaValue;

        // Set both eyes to blue
        foreach (var mat in eyesMaterialInstances)
        {
            if (mat != null)
                mat.color = Color.blue;
        }

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Positive interaction with Bear. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());
    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();
        
        Vector3 pushDirection = transform.right;

        Push(pushDirection);
        audioSource.PlayOneShot(badVoiceline);
        audioSource.PlayOneShot(hitNoise);

        // Set both eyes to red
        foreach (var mat in eyesMaterialInstances)
        {
            if (mat != null)
                mat.color = Color.red;
        }

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = negativeKarmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Positive interaction with Bear. Karma Type: " + GetKarmaType());
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

    public void Push(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
        }
    }

    public void ResetTeddyEyes()
    {
        foreach (var mat in eyesMaterialInstances)
        {
            if (mat != null)
                mat.color = originalEyeColor;
        }
    }
}
