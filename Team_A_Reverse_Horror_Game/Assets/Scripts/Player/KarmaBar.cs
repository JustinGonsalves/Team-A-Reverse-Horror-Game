using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class KarmaBar : MonoBehaviour
{

    public int maxKarma = 100;
    public KarmaManager karmaManager;
    public float sliderValue;

    public Slider slide;

    public void Start()
    {
        slide.minValue = 0;
        slide.maxValue = 100;
        
        slide.value = karmaManager.totalKarma;

    }

    public void Update()
    {
        slide.value = karmaManager.totalKarma;
    }

}
