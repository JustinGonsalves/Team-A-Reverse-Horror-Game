using UnityEngine;

public class CameraAnimatorReset : MonoBehaviour
{
    private Animator cameraAnimator;
    public string defaultStateName; // Or whatever your entry animation state is

    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
        
        if (cameraAnimator != null)
        {
            cameraAnimator.Play(defaultStateName, 0, 0f); // Reset to start of Idle
            cameraAnimator.Update(0f); // Force it to apply immediately
        }
        else
        {
            Debug.Log("Animator not found");
        }
    }
}
