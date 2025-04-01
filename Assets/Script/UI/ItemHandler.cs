using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    public Image mainIcon;

    public Slider exp;
    public Slider interest;

    public UI_Event UIEvent;
    public ItamData itemData;
    public Item_Panel panel;

    public bool isAcquired = false;
    public Text number;
    public int count;

    private string countKey;

    private void Start()
    {
        if(UIEvent != null)
        {
            UIEvent.itemSlotOpen += ItemSlot;
        }

        countKey = gameObject.name;
        Item_Count();
    }

    public void ItemSlot(object sender, EventArgs e)
    {
        if (!isAcquired)
        {
            mainIcon.sprite = itemData.item_Hide_Icon;
        }
        else
        {
            mainIcon.sprite = itemData.item_Icon;
        }
    }

    // 아이템 팝업창에 아이템 정보를 줌
    public void Panel_Set()
    {
        if (!isAcquired)
        {
            mainIcon.sprite = itemData.item_Hide_Icon;
            panel.icon.sprite = itemData.item_Hide_Icon;
            panel.itam_name.text = "???";
            panel.explanation.text = "???";
            count = 0;
            panel.count.text = count.ToString();
            panel.btn.onClick.RemoveAllListeners();
        }
        else
        {
            mainIcon.sprite = itemData.item_Icon;
            panel.itam_name.text = itemData.item_Name;
            panel.explanation.text = itemData.item_explanation;
            panel.icon.sprite = itemData.item_Icon;
            count = PlayerPrefs.GetInt(countKey);
            panel.count.text = count.ToString();
            panel.btn.onClick.RemoveAllListeners();
            panel.btn.onClick.AddListener(Item_Use);
        }
    }

    // 플레이어 프리팹에 들어있는 아이템 수를 가져옴
    public void Item_Count()
    {
        // 플레이어프리팹에 카운트값이 있는지 확인
        if (!PlayerPrefs.HasKey(countKey))
        {
            PlayerPrefs.SetInt(countKey, 0);
        }

        count = PlayerPrefs.GetInt(countKey);

        // 아이템 사용시 실행되서 카운트가 줄어듬
        panel.count.text = count.ToString();
    }

    // 아이템을 얻을때 플레이어 프리팹에있는값을 증가시킴
    public void Item_Plus()
    {
        if(!isAcquired)
        {
            isAcquired = true;
        }

        int itemCount = PlayerPrefs.GetInt(countKey);
        itemCount++;
        PlayerPrefs.SetInt(countKey, itemCount);
    }

    // 아이템 count 감소
    public void Item_Use()
    {
        // 아이템이 없으면 종료
        if(count == 0) 
        {
            Debug.Log("아이템 없음");
            return; 
        }

        // 아이템 감소
        PlayerPrefs.SetInt(countKey, --count);
        Debug.Log("아이템 사용");

        // 아이템 사용 함수
        Item_Effect_Test();

        // 아이템 숫자 갱신
        Item_Count();
    }

    public void Item_Effect_Test()
    {
        Debug.Log(gameObject.name);

        if (itemData.expIncrease)
        {
            exp.value += itemData.value;
        }
        else if (itemData.interestIncrease)
        {
            interest.value += itemData.value;
        }
        else if (itemData.typeChanged)
        { return; }

    }

    // 아이템 사용 효과
    public void Item_Effect()
    {
        Debug.Log(gameObject.name);

        switch (gameObject.name)
        {
            case "Item_1":
                exp.value += 3600f;
                Debug.Log("아이템 1 사용");
                break;

            case "Item_2":
                exp.value += 7200f;
                Debug.Log("아이템 2 사용");
                break;
            case "Item_3":
                exp.value += 10800f;
                Debug.Log("아이템 3 사용");
                break;
            case "Item_4":
                interest.value += 10f;
                Debug.Log("아이템 4 사용");
                break;
            case "Item_5":
                interest.value += 20f;
                Debug.Log("아이템 5 사용");
                break;
            case "Item_6":
                interest.value += 30f;
                Debug.Log("아이템 6 사용");
                break;
        }
    }

}
