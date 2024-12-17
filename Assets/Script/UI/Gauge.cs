using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Slider slider;
    public bool exp;

    public void MaxGauge(int max_Gauge)
    {
        slider.maxValue = max_Gauge;
    }

    public void SetGauge(int val)
    {
        slider.value = val;
    }

    public void GaugeUp()
    {
        slider.value += 20;
    }
    public void GaugeDown()
    {
        slider.value -= 10;
    }


    private void Update()
    {
        if (exp)
        {
            slider.value += Time.deltaTime * 1.0f;
            slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
        }

    }
}
