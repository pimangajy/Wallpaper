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
            Debug.Log($"캐릭터 생성 완료: {charData.CharatorName}");
        }
        else
        {
            Debug.LogWarning("해당 조건에 맞는 캐릭터 데이터를 찾을 수 없습니다.");
        }
    }
}
