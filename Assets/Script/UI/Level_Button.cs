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
        // 초기값 설정 (이미 저장된 값이 있다면 그 값을 사용하도록 수정)
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
            Debug.Log("아이템 비어있음");
            return;
        }

        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("경험치 부족");
            return;
        }

        // 레벨 가져옴
        int currentLevel = PlayerPrefs.GetInt("level");
        currentLevel++;

        // 레벨이 4가되면 0으로 초기화
        if(currentLevel >= 4)
        {
            currentLevel = 0;

            // 아이템 흭득 함수 실행
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

        // 애정도 처리
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;

        // 값 저장
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("interest", interest);
        PlayerPrefs.Save();

        // 게이지 초기화
        exp_Gauge.value = 0;
        if (interest == 1)
        {
            Interest_Gauge.value = 0;
        }

        Debug.Log($"현재 레벨: {currentLevel}, 애정도 상태: {interest}");
        Debug.Log("경험치 최대량 : " +  exp_Gauge.maxValue);
    }

    public void Item_Plus()
    {
        int ran = UnityEngine.Random.Range(0, itemlen.Length);

        // 랜덤한 아이템들중에 하나 가져옴
        ItemHandler itemHandler = itemlen[ran].GetComponent<ItemHandler>();

        // 아이템카운트 증가
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();
            Debug.Log((ran+1) + " 번쨰 아이템 흭득");
        }
    }
}
