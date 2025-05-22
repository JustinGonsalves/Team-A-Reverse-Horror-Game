using UnityEngine;

public class TestTV : Interactable
{
    public Light pointLight;
    private bool isTurnedOn = false;

    public float pushForce = 3f;

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void PositiveInteract()
    {
        if (hasBeenInteracted) return;

        isTurnedOn = true;
        hasBeenInteracted = true;

        selectedKarmaType = KarmaType.Good;
        karmaValueEffect = 16.6f;

        if (pointLight != null)
        {
            pointLight.enabled = isTurnedOn;
            Transform outline = transform.Find("Outline");

            if (outline != null)
            {
                outline.gameObject.SetActive(false);
            }
        }

        Debug.Log("Positive interaction with TV. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());

    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted) return;

        hasBeenInteracted = true;
        Vector3 pushDirection = -transform.forward;
        Push(pushDirection);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = -16.6f;

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
}
