using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundProgress : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    void Start()
    {
        // ���������� ����� �ð��� �ҷ���
        string lastTimeStr = PlayerPrefs.GetString("LastExitTime", "");
        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            System.DateTime lastTime = System.DateTime.Parse(lastTimeStr);
            System.TimeSpan timeElapsed = System.DateTime.Now - lastTime;

            // ���� �ð� ���� ������ ����
            float elapsedSeconds = (float)timeElapsed.TotalSeconds;
            exp_Gauge.value += elapsedSeconds * 1.0f;
            exp_Gauge.value = Mathf.Clamp(exp_Gauge.value, exp_Gauge.minValue, exp_Gauge.maxValue);
            PlayerPrefs.SetFloat("exp", exp_Gauge.value);

            // ���� �ð����� ������ ����
            Interest_Gauge.value -= elapsedSeconds * (10 / 3600f);
            Interest_Gauge.value = Mathf.Clamp(Interest_Gauge.value, Interest_Gauge.minValue, Interest_Gauge.maxValue);
            PlayerPrefs.SetFloat("interest", Interest_Gauge.value);

            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // �� ���� �� ���� �ð��� ����
        PlayerPrefs.SetString("LastExitTime", System.DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // ���� ��׶���� �̵��� �� ���� �ð� ����
            PlayerPrefs.SetString("LastExitTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
    }
}
