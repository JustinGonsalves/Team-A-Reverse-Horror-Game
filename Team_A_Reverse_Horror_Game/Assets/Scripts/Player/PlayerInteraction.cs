using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;
    public Animator playerAnimator;

    private Interactable currentHovered;

    private void Update()
    {
        HandleInteractionRaycast();
        HandleInteractionInput();
    }
    private void HandleInteractionRaycast()
    {
        // Create a ray starting from the camera position, shooting forwards
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Visual debug line
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);

        // Local variable to store the new hovered object
        Interactable newHovered = null;

        // Check to see if the ray hits something within the interact distance
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // If the new object has an Interactable component, store it
            newHovered = hit.collider.GetComponent<Interactable>();

            if (newHovered != null)
            {
                // If we look at a new object
                if (newHovered != currentHovered)
                {
                    // End the hover on the old object
                    currentHovered?.OnHoverEnd();
                    // Assign and start hover on the new object
                    currentHovered = newHovered;
                    currentHovered.OnHoverStart();
                }
            }
        }

        // Clear the object if nothing is hit
        if (newHovered == null && currentHovered != null)
        {
            currentHovered.OnHoverEnd();
            currentHovered = null;
        }
    }

    void HandleInteractionInput()
    {
        // If you're looking at an object that hasn't already been interacted with and press E
        if (currentHovered != null && Input.GetKeyDown(KeyCode.E) && !currentHovered.hasBeenInteracted)
        {
            playerAnimator.Play("interact");
            currentHovered.PositiveInteract();
        }

        // If you're looking at an object that hasn't already been interacted with and press E
        if (currentHovered != null && Input.GetKeyDown(KeyCode.Q) && !currentHovered.hasBeenInteracted)
        {
            playerAnimator.Play("swipe");
            currentHovered.NegativeInteract();
        }
    }
}
