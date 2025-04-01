using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Event : MonoBehaviour
{
    public event EventHandler itemSlotOpen;

    public void ItemSlotOpen()
    {
        itemSlotOpen?.Invoke(this, EventArgs.Empty);
    }
}
