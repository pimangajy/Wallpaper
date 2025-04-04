using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(5, 10)]
    public string itemExplanation;
    public Sprite itemIcon;
    public Sprite itemHideIcon;
    public bool expIncrease;
    public bool interestIncrease;
    public bool typeChanged;
    public int value;
}