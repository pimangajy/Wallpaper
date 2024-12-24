using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Level_Button : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    private void Start()
    {
        // �ʱⰪ ���� (�̹� ����� ���� �ִٸ� �� ���� ����ϵ��� ����)
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.SetInt("interest", 0);
            PlayerPrefs.Save();
        }
    }
    public void Level_Up()
    {
        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("����ġ ����");
            return;
        }

        // ���� ������
        int currentLevel = PlayerPrefs.GetInt("level");
        currentLevel++;

        // ������ 4���Ǹ� 0���� �ʱ�ȭ
        if(currentLevel >= 4)
        {
            currentLevel = 0;
        }

        // ������ ó��
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;

        // �� ����
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("interest", interest);
        PlayerPrefs.Save();

        // ������ �ʱ�ȭ
        exp_Gauge.value = 0;
        if (interest == 1)
        {
            Interest_Gauge.value = 0;
        }

        Debug.Log($"���� ����: {currentLevel}, ������ ����: {interest}");
    }
}
