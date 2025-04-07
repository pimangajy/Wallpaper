using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharator : MonoBehaviour
{
    public CharatorList CharatorList;

    void Start()
    {
        int savedLevel = PlayerPrefs.GetInt("level");
        int savedType = PlayerPrefs.GetInt("Character_type");

        CharatorData.ChatatorType type = (CharatorData.ChatatorType)savedType;

        CharatorData charData = CharatorList.GetCharatorData(savedLevel, type);

        if (charData != null)
        {
            Instantiate(charData.Charator, transform.position, Quaternion.identity);
            Debug.Log($"ĳ���� ���� �Ϸ�: {charData.CharatorName}");
        }
        else
        {
            Debug.LogWarning("�ش� ���ǿ� �´� ĳ���� �����͸� ã�� �� �����ϴ�.");
        }
    }
}
