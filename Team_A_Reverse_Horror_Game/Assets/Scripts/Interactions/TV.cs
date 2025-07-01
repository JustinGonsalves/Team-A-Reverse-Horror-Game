using UnityEngine;
using UnityEngine.Video;

public class TV : Interactable
{
    public Light pointLight;
    public float pushForce = 3f;
    public AudioClip toggleSwitchNoise;
    public AudioClip crashSound;
    public float positiveKarmaValue = -20f;
    public float negativeKarmaValue = 20f;

    public Enemy enemyScript;

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


    public override void OnStageActivated()
    {
        TurnOnTV();
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.PositiveInteract();

        AudioSource.PlayClipAtPoint(toggleSwitchNoise, transform.position);
        TurnOffTV();

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = positiveKarmaValue;

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
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        Vector3 pushDirection =  transform.forward;

        AudioSource.PlayClipAtPoint(crashSound, transform.position);
        TurnOffTV();
        Push(pushDirection);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = negativeKarmaValue;

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

    private void TurnOnTV()
    {
        if (pointLight != null && videoPlayer != null)
        {
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
            enemyScript.EnableAI();
        }
    }
}
