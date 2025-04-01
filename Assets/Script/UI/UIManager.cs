using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instans;

    public static UIManager Instance
    { 
        get 
        { 
            if(instans == null)
            {
                return null;
            }
            return instans;
        } 
    }

    private Stack<GameObject> openedUI = new Stack<GameObject>();

    private void Awake()
    {
        if (instans == null)
        {
            instans = this;

            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void OpenUI(GameObject uiObject)
    {
        openedUI.Push(uiObject);
    }

    public void CloseUI()
    {
        if (openedUI.Count > 0)
        {
            GameObject closedUI = openedUI.Pop();
        }
    }

    public void SaveData(float exp, float interes)
    {
        PlayerPrefs.SetFloat("exp", exp);
        PlayerPrefs.SetFloat("interes", interes);
        PlayerPrefs.Save();
    }
}
