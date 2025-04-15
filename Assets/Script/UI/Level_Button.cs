using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static Level_Button;

public class Level_Button : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    public GameObject[] itemlen;
    public List<ItemHandler> itemhandlers = new List<ItemHandler>();

    public Gauge gauge;

    [SerializeField]
    private int character_Level = 0;

    public ChataterType characterType  = ChataterType.Default; // �ʱ� Ÿ��

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
    public void LevelUp()
    {
        character_Level = PlayerPrefs.GetInt("level");
        character_Level++;

        if (character_Level > 3) // 3���� �ʰ� �� �ʱ�ȭ
        {
            characterType = ChataterType.Default;
            character_Level = 0;
            PlayerPrefs.SetInt("Character_type", (int)characterType);
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.Save();
            Debug.Log("�ִ� ���� �ʰ�! �⺻ Ÿ������ �ʱ�ȭ.");
            Item_Plus();
        }
        else if (character_Level == 1 && characterType == ChataterType.Default)
        {
            characterType = GetRandomCharacterType(); // 1�����̸� ���� Ÿ�� �Ҵ�
            PlayerPrefs.SetInt("Character_type", (int)characterType);
            PlayerPrefs.SetInt("level", character_Level);
            PlayerPrefs.Save();
            Debug.Log($"���� 1�� �Ǿ� ���� Ÿ�� ����: {characterType}");
        }
        else
        {
            PlayerPrefs.SetInt("Character_type", (int)characterType);
            PlayerPrefs.SetInt("level", character_Level);
            PlayerPrefs.Save();
            Debug.Log($"������!  ���� Ÿ��: {characterType}, {(int)characterType}, ����: {character_Level}");
        }

        CharaterDataManager.Instance.AddCharater(CharacterAssetManager.Instance.GetCharatorData(character_Level, characterType));

        switch (character_Level)
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
            default:
                Debug.LogWarning($"�� �� ���� ���� ��: {character_Level}");
                break;
        }
    }
    private ChataterType GetRandomCharacterType()
    {
        ChataterType[] types = { ChataterType.yuniNomal};
        return types[UnityEngine.Random.Range(0, types.Length)];
    }
    public void ButtonClick()
    {
        // ������ �߰� Ȯ�� �ڵ�
        if (itemlen.Length == 0 || itemlen == null)
        {
            Debug.Log("������ �������");
            return;
        }

        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("����ġ ����");
            return;
        }

        LevelUp();

        // ������ ó��
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;
        if (Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Interest_Gauge.value = 0;
            PlayerPrefs.SetInt("interest", 1); // ������ ���� ����
        }
        else
        {
            PlayerPrefs.SetInt("interest", 0);
        }

        // ������ �ʱ�ȭ
        exp_Gauge.value = 0;
        if (interest == 1)
        {
            Interest_Gauge.value = 0;
        }
    }

    public void Item_Plus()
    {
        if (itemhandlers.Count == 0)
        {
            Debug.Log("�������� �����ϴ�.");
            return;
        }

        int ran = UnityEngine.Random.Range(0, itemhandlers.Count);

        // ������ �����۵��߿� �ϳ� ������
        ItemHandler itemHandler = itemhandlers[ran];

        // ������ī��Ʈ ����
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();

            Debug.Log((ran+1) + " ���� ������ ŉ��");
        }
    }
}
