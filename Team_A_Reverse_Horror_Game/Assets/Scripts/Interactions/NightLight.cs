using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class NightLight : Interactable
{

    // Reference to light child object, assigned in editor
    public Light pointLight;
    // Reference to off click sound
    public AudioClip switchOffSound;
    // Reference to the game object renderer to access the material properties
    public Renderer objectRenderer;
    // Emissive intensity multiplier to be applied when interacted
    public float intensityMultiplier = 2f;
    // Force of push if negative interaction
    public float pushForce = 3f;
    public float karmaValue = -20f;

    // Tracks light's on/off status
    private bool isTurnedOn = false;
    // Variable to hold an instance of the material as to not permenantly change original
    private Material materialInstance;
    //  Variable to hold the intensity value
    private Color baseEmission;

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
        // Clone the lamps material to avoid modifying the main asset
        materialInstance = objectRenderer.material;
        // Store the original emission colour at the start
        baseEmission = materialInstance.GetColor("_EmissionColor");
        // Apply the default emission to the instance
        materialInstance.SetColor("_EmissionColor", baseEmission);
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

        isTurnedOn = !isTurnedOn;

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = karmaValue;

        if (pointLight != null)
        {
            // Play click noise
            AudioSource.PlayClipAtPoint(switchOffSound, transform.position);
            // Turn on light
            pointLight.enabled = isTurnedOn;
            // Increase the emission intensity by the intensityMultiplier
            materialInstance.SetColor("_EmissionColor", baseEmission * intensityMultiplier);

            Transform outline = transform.Find("Outline");

            if (outline != null)
            {
                outline.gameObject.SetActive(false);

            }
        }

        Debug.Log("Turned on the Night Light. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());

    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        Vector3 pushDirection = -transform.forward;
        Push(pushDirection);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = 20f;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("You pushed the NightLight!. Karma Type: " + GetKarmaType());
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
}
