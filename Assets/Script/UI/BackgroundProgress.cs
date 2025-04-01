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
        // 마지막으로 저장된 시간을 불러옴
        string lastTimeStr = PlayerPrefs.GetString("LastExitTime", "");
        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            System.DateTime lastTime = System.DateTime.Parse(lastTimeStr);
            System.TimeSpan timeElapsed = System.DateTime.Now - lastTime;

            // 지난 시간 동안 게이지 증가
            float elapsedSeconds = (float)timeElapsed.TotalSeconds;
            exp_Gauge.value += elapsedSeconds * 1.0f;
            exp_Gauge.value = Mathf.Clamp(exp_Gauge.value, exp_Gauge.minValue, exp_Gauge.maxValue);
            PlayerPrefs.SetFloat("exp", exp_Gauge.value);

            // 지난 시간동안 애정도 감소
            Interest_Gauge.value -= elapsedSeconds * (10 / 3600f);
            Interest_Gauge.value = Mathf.Clamp(Interest_Gauge.value, Interest_Gauge.minValue, Interest_Gauge.maxValue);
            PlayerPrefs.SetFloat("interest", Interest_Gauge.value);

            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // 앱 종료 시 현재 시간을 저장
        PlayerPrefs.SetString("LastExitTime", System.DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // 앱이 백그라운드로 이동할 때 현재 시간 저장
            PlayerPrefs.SetString("LastExitTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
    }
}
