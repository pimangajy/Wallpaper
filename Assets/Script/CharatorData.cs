using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharatorData", menuName = "Scriptable Objects/Charator Data")]
public class CharatorData : ScriptableObject
{
    public string age;
    public string birthday;
    public string mbti;
    [TextArea(5, 10)]
    public string explanation;
    public Sprite icon_Image;
    public Sprite main_Image;
}
