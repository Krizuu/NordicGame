using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxValue(int max)
    {
        slider.maxValue = max;
    }

    public void setValue(int value)
    {
        slider.value = value;
    }
}
