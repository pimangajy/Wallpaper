using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Level_Button : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    public GameObject[] itemlen;

    public Gauge gauge;

    public GameObject[] Level_0;
    public GameObject[] Level_1;
    public GameObject[] Level_2;    
    public GameObject[] Level_3;

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
        if (itemlen == null)
        {
            Debug.Log("������ �������");
            return;
        }

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

            // ������ ŉ�� �Լ� ����
            Item_Plus();
        }

        switch (currentLevel)
        {
            case 0:
                gauge.MaxGauge(3600);
                break;

            case 1:
                gauge.MaxGauge(7200);
                break;

            case 2:
                gauge.MaxGauge(10800);
                break;

            case 3:
                gauge.MaxGauge(18000);
                break;

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
        Debug.Log("����ġ �ִ뷮 : " +  exp_Gauge.maxValue);
    }

    public void Item_Plus()
    {
        int ran = UnityEngine.Random.Range(0, itemlen.Length);

        // ������ �����۵��߿� �ϳ� ������
        ItemHandler itemHandler = itemlen[ran].GetComponent<ItemHandler>();

        // ������ī��Ʈ ����
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();
            Debug.Log((ran+1) + " ���� ������ ŉ��");
        }
    }
}
