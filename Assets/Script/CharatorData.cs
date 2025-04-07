using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharatorData", menuName = "Scriptable Objects/Charator Data")]
public class CharatorData : ScriptableObject
{
    public GameObject Charator;
    public string CharatorName;
    public string age;
    public string birthday;
    public string mbti;
    [TextArea(5, 10)]
    public string explanation;
    public Sprite main_Image;

    public int level;
    public enum ChatatorType
    {
        Default,
        yuniNomal
    }
    public ChatatorType type;
}
