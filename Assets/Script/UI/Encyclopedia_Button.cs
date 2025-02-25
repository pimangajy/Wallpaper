using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia_Button : MonoBehaviour
{
    public Encyclopedia_Panel panel;

    public string character_name;
    public string age;
    public string birthday;
    public string mbti;
    [TextArea(1, 10)]
    public string explanation;
    public int count;
    public Sprite image;

    public void Open()
    {
        panel.character_name.text = character_name;
        panel.age.text = age;
        panel.birthday.text = birthday;
        panel.mbti.text = mbti;
        panel.explanation.text = explanation;
        panel.count.text = count.ToString();
        panel.image.sprite = image;
    }
}
