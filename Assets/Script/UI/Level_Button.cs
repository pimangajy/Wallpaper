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
    public enum Charater_type 
    {
        yuniNomal,
        Default
    }
    [SerializeField]
    private Charater_type charater_Type = Charater_type.yuniNomal;
    [SerializeField]
    private int character_Level = 0;

    public enum CharacterType { A, B, C, Default }
    public Charater_type characterType = Charater_type.Default; // 초기 타입

    private void Start()
    {
        // 초기값 설정 (이미 저장된 값이 있다면 그 값을 사용하도록 수정)
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

        if (character_Level > 3) // 3레벨 초과 시 초기화
        {
            character_Level = 0;
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.Save();
            characterType = Charater_type.Default;
            Debug.Log("최대 레벨 초과! 기본 타입으로 초기화.");
            Item_Plus();
        }
        else if (character_Level == 1 && characterType == Charater_type.Default)
        {
            characterType = GetRandomCharacterType(); // 1레벨이면 랜덤 타입 할당
            PlayerPrefs.SetInt("Character_type", (int)characterType);
            PlayerPrefs.Save();
            Debug.Log($"레벨 1이 되어 랜덤 타입 변경: {characterType}");
        }
        else
        {
            PlayerPrefs.SetInt("level", character_Level);
            PlayerPrefs.Save();
            Debug.Log($"현재 타입: {characterType}, 레벨: {character_Level}");
        }

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
                Debug.LogWarning($"알 수 없는 레벨 값: {character_Level}");
                break;
        }
    }
    private Charater_type GetRandomCharacterType()
    {
        Charater_type[] types = { Charater_type.yuniNomal};
        return types[UnityEngine.Random.Range(0, types.Length)];
    }
    public void ButtonClick()
    {
        // 아이템 추가 확인 코드
        if (itemlen.Length == 0 || itemlen == null)
        {
            Debug.Log("아이템 비어있음");
            return;
        }

        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("경험치 부족");
            return;
        }

        LevelUp();

        // 애정도 처리
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;
        if (Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Interest_Gauge.value = 0;
            PlayerPrefs.SetInt("interest", 1); // 애정도 상태 저장
        }
        else
        {
            PlayerPrefs.SetInt("interest", 0);
        }

        // 게이지 초기화
        exp_Gauge.value = 0;
        if (interest == 1)
        {
            Interest_Gauge.value = 0;
        }

        Debug.Log($"현재 레벨: {character_Level}, 애정도 상태: {interest}");
        Debug.Log("경험치 최대량 : " +  exp_Gauge.maxValue);
    }

    public void Item_Plus()
    {
        if (itemhandlers.Count == 0)
        {
            Debug.Log("아이템이 없습니다.");
            return;
        }

        int ran = UnityEngine.Random.Range(0, itemhandlers.Count);

        // 랜덤한 아이템들중에 하나 가져옴
        ItemHandler itemHandler = itemhandlers[ran];

        // 아이템카운트 증가
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();
            if (!itemHandler.isAcquired)
            {
                itemHandler.isAcquired = true;
            }
            Debug.Log((ran+1) + " 번쨰 아이템 흭득");
        }
    }
}
