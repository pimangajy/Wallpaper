using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item Data")]
public class ItamData : ScriptableObject
{
    public string item_Name;
    public string item_explanation;
    public Sprite item_Icon;
    public Sprite item_Hide_Icon;

    public bool expIncrease;
    public bool interestIncrease;
    public bool typeChanged;
    public int value;
}
