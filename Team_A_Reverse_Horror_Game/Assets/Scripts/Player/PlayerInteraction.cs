using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Length of player interaction
    public float interactDistance = 3f;
    public Camera playerCamera;
    public Animator playerAnimator;
    public AudioClip gurgleNoise;

    // Slot for current interactable looked at
    public Interactable currentHovered;
    public Interactable pendingInteraction;
    private KarmaManager karmaManager;
    
    private void Start()
    {
        // Find and assign KarmaManager script
        karmaManager = GetComponent<KarmaManager>();
    }

    private void Update()
    {
        // Each frame run raycast and interact methods
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
        // If you're looking at an object that hasn't already been interacted with and is in the current round of objectives
        if (currentHovered != null && !currentHovered.hasBeenInteracted && currentHovered.canBeInteracted)
        {
            // And press E
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHovered.SupportsPositiveInteraction)
                {
                    pendingInteraction = currentHovered;
                    playerAnimator.Play("interact");
                    //currentHovered.PositiveInteract();
                    //karmaManager.ApplyKarmaFromInteractable(currentHovered.GetKarmaValue(), currentHovered.GetKarmaType());
                }
            }

        }

        // If you're looking at an object that hasn't already been interacted with and is in the current round of objectives
        if (currentHovered != null && !currentHovered.hasBeenInteracted && currentHovered.canBeInteracted)
        {
            // And press Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentHovered.SupportsNegativeInteraction)
                {
                    if (currentHovered is GurgleInteraction)
                    {
                        pendingInteraction = currentHovered;
                        currentHovered.NegativeInteract();
                        karmaManager.ApplyKarmaFromInteractable(currentHovered.GetKarmaValue(), currentHovered.GetKarmaType());
                    }
                    else
                    {
                        pendingInteraction = currentHovered;
                        playerAnimator.Play("swipe");
                    }
                }
            }

        }
    }

    public void ClearHoveredReference()
    {
        currentHovered = null;
    }
}
