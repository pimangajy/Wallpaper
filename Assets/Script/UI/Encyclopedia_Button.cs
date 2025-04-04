using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Encyclopedia_Button : MonoBehaviour
{
    public Encyclopedia_Panel panel;
    [SerializeField]
    private CharatorData charatorData;
    [SerializeField]
    private ItemData itemData;

    public GameObject icon;
    public GameObject charatorName;

    public void Open()
    {
        panel.character_name.text = charatorData.name;
        panel.age.text = charatorData.age;
        panel.birthday.text = charatorData.birthday;
        panel.mbti.text = charatorData.mbti;
        panel.explanation.text = charatorData.explanation;
        // panel.count.text = count.ToString();
        panel.main_image.sprite = charatorData.main_Image;

    }
}
