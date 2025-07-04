using StarterAssets;
using UnityEngine;

public class CreakyFloorboards : MonoBehaviour
{
    public AudioClip creakSound;
    public Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FirstPersonController playerController = other.GetComponent<FirstPersonController>();

            if (playerController != null && !playerController._isWalking)
            {
                Debug.Log("Stepped on CreakyFloor");
                AudioSource.PlayClipAtPoint(creakSound, transform.position);

                Debug.LogWarning("Calling StandAtStairs from Interactables.cs!");
                enemy.standAtStairsTriggered = true;
            }
        }
    }
}
