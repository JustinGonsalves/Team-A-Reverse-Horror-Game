using UnityEngine;
using UnityEngine.UI;



public class KarmaBar : MonoBehaviour
{
public int maxKarma = 100;
public KarmaManager karmaManager;
    public Slider slide;
    public void Start()
    {
        slide.minValue = 0;
        slide.maxValue = 100;

        slide.value = karmaManager.totalKarma;
    }

    public void UpdateKarmaSlider()
    {

    }
}
