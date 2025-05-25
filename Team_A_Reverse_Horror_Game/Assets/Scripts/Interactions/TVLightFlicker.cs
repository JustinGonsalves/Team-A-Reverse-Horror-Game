using System.Collections;
using UnityEngine;

public class TVLightFlicker : MonoBehaviour
{
    public Light tvLight;
    public float flickerRate = 0.5f;
    public float minIntensity = 1.37f;
    public float maxIntensity = 2.95f;

    private Coroutine flickerRoutine;

    
    public void StartFlicker()
    {
        flickerRoutine = StartCoroutine(Flicker());
    }

    public void StopFlicker()
    {
        StopCoroutine(flickerRoutine);
        flickerRoutine = null;
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            float baseTargetItensity = Random.Range(minIntensity, maxIntensity);
            float baseStartIntensity = tvLight.intensity;

            float t = 0;

            while (t < flickerRate)
            {
                t += Time.deltaTime;
                tvLight.intensity = Mathf.Lerp(baseStartIntensity, baseTargetItensity, t / flickerRate);
                yield return null;
            }
        }
    }
}
