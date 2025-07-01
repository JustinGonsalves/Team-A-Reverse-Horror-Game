using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public GameObject player;
    public float detectionDistance = 3f;

    public AudioClip woodFootstep;
    public AudioClip carpetFootstep;
    public AudioSource audioSource;

    private SurfaceType surfaceType;
    private enum SurfaceType { Carpet, Wood }

    private CharacterController characterController;

    private void Awake()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        SurfaceDetectionRaycast();

        if (IsPlayerMoving())
        {
            PlayFootsteps();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    private bool IsPlayerMoving()
    {
        // Assumes CharacterController is used for movement
        return characterController != null && characterController.velocity.magnitude > 0.1f;
    }

    private void SurfaceDetectionRaycast()
    {
        Ray ray = new Ray(player.transform.position, -player.transform.up);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.blue);

        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("WoodFloor"))
            {
                surfaceType = SurfaceType.Wood;
            }
            else if (hit.collider.CompareTag("CarpetFloor"))
            {
                surfaceType = SurfaceType.Carpet;
            }
        }
    }

    public void PlayFootsteps()
    {
        AudioClip targetClip = null;
        switch (surfaceType)
        {
            case SurfaceType.Wood:
                targetClip = woodFootstep;
                break;
            case SurfaceType.Carpet:
                targetClip = carpetFootstep;
                break;
        }

        if (audioSource.clip != targetClip)
        {
            audioSource.clip = targetClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
