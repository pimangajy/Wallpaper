using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatorList : MonoBehaviour
{
    public List<CharatorData> charatorDatas; // �ν����Ϳ� ScriptableObject�� ���


    public CharatorData GetCharatorData(int level, CharatorData.ChatatorType type)
    {
        return charatorDatas.Find(c => c.level == level && c.type == type);
    }
}
