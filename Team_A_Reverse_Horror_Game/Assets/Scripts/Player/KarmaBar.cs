using UnityEngine;
using UnityEngine.UI;



public class KarmaBar : MonoBehaviour
{
public int maxKarma = 100;

    public Slider slider;
    public void Start()
    {
        slider.maxValue = maxKarma;
        
    }
}
