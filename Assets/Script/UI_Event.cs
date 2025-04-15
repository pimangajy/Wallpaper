using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Event : MonoBehaviour
{
    public event EventHandler itemSlotOpen;
    public event EventHandler encyclopediaSlotOpen;

    public void Start()
    {
        ItemSlotOpen();
        EncyclopediaSlotOpen();
    }
    public void ItemSlotOpen()
    {
        itemSlotOpen?.Invoke(this, EventArgs.Empty);
    }

    public void EncyclopediaSlotOpen()
    {
        encyclopediaSlotOpen?.Invoke(this, EventArgs.Empty);
    }

}
