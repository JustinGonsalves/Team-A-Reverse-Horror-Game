using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    [Header("Vignette Effect Settings")]
    public float effectIntensity = 0.549f;
    public float negEffectDuration = 1.35f;
    public float posEffectDuraction = 2.3f;

    private static readonly Color DefaultVignetteColor = Color.black;
    private const float DefaultVignetteIntensity = 0.105f;

    private Volume postProcessVolume;

    void Awake()
    {
        // Find the first active Volume in the scene
        postProcessVolume = FindFirstObjectByType<Volume>();
    }

    public void PlayRedVignetteEffect()
    {
        PlayVignetteEffect(Color.red, effectIntensity, negEffectDuration);
    }

    public void PlayBlueVignetteEffect()
    {
        PlayVignetteEffect(Color.blue, effectIntensity, posEffectDuraction);
    }

    public void PlayVignetteEffect(Color targetColor, float targetIntensity, float duration)
    {
        if (postProcessVolume != null)
            StartCoroutine(VignetteEffectCoroutine(targetColor, targetIntensity, duration));
    }

    private IEnumerator VignetteEffectCoroutine(Color targetColor, float targetIntensity, float totalDuration)
    {
        if (!postProcessVolume.profile.TryGet<Vignette>(out var vignette))
            yield break;

        Color originalColor = vignette.color.value;
        float originalIntensity = vignette.intensity.value;

        float halfDuration = totalDuration * 0.5f;
        float t = 0f;

        // Lerp to effect
        while (t < halfDuration)
        {
            float lerpT = t / halfDuration;
            vignette.color.value = Color.Lerp(originalColor, targetColor, lerpT);
            vignette.intensity.value = Mathf.Lerp(originalIntensity, targetIntensity, lerpT);
            t += Time.deltaTime;
            yield return null;
        }
        vignette.color.value = targetColor;
        vignette.intensity.value = targetIntensity;

        // Lerp back to default
        t = 0f;
        while (t < halfDuration)
        {
            float lerpT = t / halfDuration;
            vignette.color.value = Color.Lerp(targetColor, DefaultVignetteColor, lerpT);
            vignette.intensity.value = Mathf.Lerp(targetIntensity, DefaultVignetteIntensity, lerpT);
            t += Time.deltaTime;
            yield return null;
        }
        vignette.color.value = DefaultVignetteColor;
        vignette.intensity.value = DefaultVignetteIntensity;
    }
}
