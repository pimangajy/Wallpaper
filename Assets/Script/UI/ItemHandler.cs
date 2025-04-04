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
    public ItemData itemData;
    public Item_Panel panel;
    private string countKey;
    private int count;

    private void Start()
    {
        if (UIEvent != null)
        {
            UIEvent.itemSlotOpen += ItemSlot;
        }
        countKey = gameObject.name;
        LoadItemCount();
    }

    private void ItemSlot(object sender, EventArgs e)
    {
        bool discovered = ItemDataManager.Instance.IsItemDiscovered(itemData);
        mainIcon.sprite = discovered ? itemData.itemIcon : itemData.itemHideIcon;
    }

    public void Panel_Set()
    {
        bool discovered = ItemDataManager.Instance.IsItemDiscovered(itemData);
        panel.icon.sprite = discovered ? itemData.itemIcon : itemData.itemHideIcon;
        panel.itam_name.text = discovered ? itemData.itemName : "???";
        panel.explanation.text = discovered ? itemData.itemExplanation : "???";
        panel.count.text = count.ToString();
        panel.btn.onClick.RemoveAllListeners();
        if (discovered) panel.btn.onClick.AddListener(Item_Use);
    }

    private void LoadItemCount()
    {
        panel.count.text = ItemDataManager.Instance.GetItemCount(itemData).ToString();
    }

    public void Item_Plus()
    {
        ItemDataManager.Instance.AddItem(itemData, 1);
        panel.count.text = ItemDataManager.Instance.GetItemCount(itemData).ToString();
        Debug.Log(itemData.itemName);
    }

    public void Item_Use()
    {
        ItemDataManager.Instance.UseItem(itemData);

        if (count <= 0)
        {
            Debug.Log("아이템 없음");
            return;
        }
        PlayerPrefs.SetInt(countKey, --count);
        ApplyItemEffect();
        LoadItemCount();
    }

    private void ApplyItemEffect()
    {
        if (itemData.expIncrease) exp.value += itemData.value;
        else if (itemData.interestIncrease) interest.value += itemData.value;
        Debug.Log($"{itemData.itemName} 사용! 효과 적용 완료");
    }
}

