using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TestVase : Interactable
{
    public float pushForce = 3f;
    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;

        Transform outline = transform.Find("Outline");
        hasBeenInteracted = true;

        outline.gameObject.SetActive(false);
    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted) return;

        Vector3 pushDirection = -transform.forward;
        Transform outline = transform.Find("Outline");
        hasBeenInteracted = true;

        Push(pushDirection);
        outline.gameObject.SetActive(false);
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
}
