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

    // ������ �˾�â�� ������ ������ ��
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

    // �÷��̾� �����տ� ����ִ� ������ ���� ������
    public void Item_Count()
    {
        // �÷��̾������տ� ī��Ʈ���� �ִ��� Ȯ��
        if (!PlayerPrefs.HasKey(countKey))
        {
            PlayerPrefs.SetInt(countKey, 0);
        }

        count = PlayerPrefs.GetInt(countKey);

        // ������ ���� ����Ǽ� ī��Ʈ�� �پ��
        panel.count.text = count.ToString();
    }

    // �������� ������ �÷��̾� �����տ��ִ°��� ������Ŵ
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

    // ������ count ����
    public void Item_Use()
    {
        // �������� ������ ����
        if(count == 0) 
        {
            Debug.Log("������ ����");
            return; 
        }

        // ������ ����
        PlayerPrefs.SetInt(countKey, --count);
        Debug.Log("������ ���");

        // ������ ��� �Լ�
        Item_Effect_Test();

        // ������ ���� ����
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

    // ������ ��� ȿ��
    public void Item_Effect()
    {
        Debug.Log(gameObject.name);

        switch (gameObject.name)
        {
            case "Item_1":
                exp.value += 3600f;
                Debug.Log("������ 1 ���");
                break;

            case "Item_2":
                exp.value += 7200f;
                Debug.Log("������ 2 ���");
                break;
            case "Item_3":
                exp.value += 10800f;
                Debug.Log("������ 3 ���");
                break;
            case "Item_4":
                interest.value += 10f;
                Debug.Log("������ 4 ���");
                break;
            case "Item_5":
                interest.value += 20f;
                Debug.Log("������ 5 ���");
                break;
            case "Item_6":
                interest.value += 30f;
                Debug.Log("������ 6 ���");
                break;
        }
    }

}
