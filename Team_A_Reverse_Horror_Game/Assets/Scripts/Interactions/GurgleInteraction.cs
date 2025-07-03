using System.Collections;
using UnityEngine;

public class GurgleInteraction : Interactable
{
    public override bool SupportsPositiveInteraction => false;

    public GameObject player;
    public AudioClip gurgleNoise;
    public Light bedroomLight;
    public float karmaValue = 20;

    private float defaultTemperature = 6570f;

    public override KarmaType GetKarmaType()
    {
        return base.GetKarmaType();
    }

    public override float GetKarmaValue()
    {
        return base.GetKarmaValue();
    }

    public override void NegativeInteract()
    {
        if (hasBeenInteracted || !canBeInteracted) return;

        base.NegativeInteract();

        AudioSource.PlayClipAtPoint(gurgleNoise, transform.position);

        selectedKarmaType = KarmaType.Bad;
        karmaValueEffect = karmaValue;

        Transform outline = transform.Find("Outline");

        if (outline != null)
        {
            outline.gameObject.SetActive(false);
        }

        // Start cycling the color temperature
        if (bedroomLight != null && gurgleNoise != null)
        {
            StartCoroutine(CycleColourTemperature());
        }

        Debug.Log("Gurgled at child. Karma Type: " + GetKarmaType());
        Debug.Log("Karma Change = " + GetKarmaValue());
    }

    private IEnumerator CycleColourTemperature()
    {
        float minTemp = 1500f;
        float maxTemp = 3604f;
        float duration = gurgleNoise.length;
        float elapsed = 0f;
        float speed = 17f;

        bedroomLight.useColorTemperature = true;

        while (elapsed < duration)
        {
            // move between minTemp and maxTemp using a sine wave
            float t = Mathf.Sin(elapsed * speed) * 0.5f + 0.5f;
            bedroomLight.colorTemperature = Mathf.Lerp(minTemp, maxTemp, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        bedroomLight.colorTemperature = defaultTemperature;
        gameObject.SetActive(false);
    }

    public override void OnHoverStart()
    {
        base.OnHoverStart();
    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }

}
