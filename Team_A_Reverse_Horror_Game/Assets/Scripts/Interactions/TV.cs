using UnityEngine;
using UnityEngine.Video;

public class TV : Interactable
{
    public Light pointLight;
    public float pushForce = 3f;
    public AudioClip toggleSwitchNoise;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

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
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the box isn't already playing and hasn't been interacted with already
        if (!hasBeenInteracted)
        {
            // If the collider has the Player tag
            if (other.gameObject.CompareTag("Player"))
            {
                // Turn on TV
                TurnOnTV();
            }
        }
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;
        hasBeenInteracted = true;

        AudioSource.PlayClipAtPoint(toggleSwitchNoise, transform.position);
        TurnOffTV();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = -20f;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Positive interaction with TV. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());

    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted) return;

        hasBeenInteracted = true;
        Vector3 pushDirection =  transform.forward;

        TurnOffTV();
        Push(pushDirection);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = 20f;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        Debug.Log("Negative interaction with TV. Karma Type: " + GetKarmaType());
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

    public void Push(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
        }
    }

    private void TurnOnTV()
    {
        if (pointLight != null && videoPlayer != null)
        {
            AudioSource.PlayClipAtPoint(toggleSwitchNoise, transform.position);
            pointLight.enabled = true;
            GetComponent<TVLightFlicker>().StartFlicker();
            videoPlayer.Play();
            audioSource.Play();
            
        }
    }

    private void TurnOffTV()
    {
        if (pointLight != null && videoPlayer != null)
        {
            pointLight.enabled = false;
            GetComponent<TVLightFlicker>().StopFlicker();
            videoPlayer.Stop();
            audioSource.Stop();
        }
    }
}
