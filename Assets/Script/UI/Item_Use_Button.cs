using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Use_Button : MonoBehaviour
{
    public string CountKey;
    public void Use()
    {
        int count = PlayerPrefs.GetInt(CountKey);

        if (count > 0)
        {

        }
    }
}
