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
            Debug.Log("����ġ ����");
            Debug.Log(exp_Gauge.value + " " + exp_Gauge.maxValue);
        }else if (exp_Gauge.value >= exp_Gauge.maxValue && Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Debug.Log("������ & ������ �ִ�");
        }else if(exp_Gauge.value >= exp_Gauge.maxValue && Interest_Gauge.value < Interest_Gauge.maxValue)
        {
            Debug.Log("������ & ������ ����");
        }
    }
}
