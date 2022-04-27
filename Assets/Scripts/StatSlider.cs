using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSlider : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    [SerializeField] public float _value = 100;

    public void SetMaxValue(float _value)
    {
        slider.maxValue = _value;
        slider.value = _value;
    }
    
    public void SetSliderValue(float _value)
    {
        slider.value = _value;
    }
}
