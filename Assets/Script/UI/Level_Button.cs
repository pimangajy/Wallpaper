using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Button : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    public void Level_Up()
    {
        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("경험치 부족");
            Debug.Log(exp_Gauge.value + " " + exp_Gauge.maxValue);
        }else if (exp_Gauge.value >= exp_Gauge.maxValue && Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Debug.Log("레벨업 & 애정도 최대");
        }else if(exp_Gauge.value >= exp_Gauge.maxValue && Interest_Gauge.value < Interest_Gauge.maxValue)
        {
            Debug.Log("레벨업 & 애정도 부족");
        }
    }
}
