using UnityEngine;

public class DripSoundOnCollision : MonoBehaviour
{
    public AudioClip dripSound;
    public string waterTag = "Water"; // Tag your water object as "Water"
    private AudioSource audioSource;
    private ParticleSystem partSystem;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        partSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(waterTag) && dripSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dripSound);
        }
    }
}
