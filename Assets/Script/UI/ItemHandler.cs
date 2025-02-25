using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    public Slider exp;
    public Slider interest;

    public string item_Name;
    public string item_explanation;
    public Sprite item_Icon;
    public Item_Panel panel;
    private string item_Title;


    public Text number;
    public int count;

    private string countKey;

    private void Start()
    {
        countKey = gameObject.name;
        Item_Count();
    }

    // ������ �˾�â�� ������ ������ ��
    public void Panel_Set()
    {
        panel.itam_name.text = item_Name;
        panel.explanation.text = item_explanation;
        panel.icon.sprite = item_Icon;
        count = PlayerPrefs.GetInt(countKey);
        panel.count.text = count.ToString();
        panel.btn.onClick.RemoveAllListeners();
        panel.btn.onClick.AddListener(Item_Use);
    }

    // �÷��̾� �����տ� ����ִ� ������ ���� ������
    public void Item_Count()
    {
        // �÷��̾������տ� ī��Ʈ���� �ִ��� Ȯ��
        if (!PlayerPrefs.HasKey(countKey))
        {
            PlayerPrefs.SetInt(countKey, 0);
        }

        count = PlayerPrefs.GetInt(countKey);
        panel.count.text = count.ToString();
    }

    // �������� ������ �÷��̾� �����տ��ִ°��� ������Ŵ
    public void Item_Plus()
    {
        int itemCount = PlayerPrefs.GetInt(countKey);
        itemCount++;
        PlayerPrefs.SetInt(countKey, itemCount);
    }

    // ������ count ����
    public void Item_Use()
    {
        // �������� ������ ����
        if(count == 0) 
        {
            Debug.Log("������ ����");
            return; 
        }

        // ������ ����
        PlayerPrefs.SetInt(countKey, --count);
        Debug.Log("������ ���");

        // ������ ��� �Լ�
        Item_Effect();

        // ������ ���� ����
        Item_Count();
    }

    // ������ ��� ȿ��
    public void Item_Effect()
    {
        Debug.Log(gameObject.name);

        switch (gameObject.name)
        {
            case "Item_1":
                exp.value += 3600f;
                Debug.Log("������ 1 ���");
                break;

            case "Item_2":
                exp.value += 7200f;
                Debug.Log("������ 2 ���");
                break;
            case "Item_3":
                exp.value += 10800f;
                Debug.Log("������ 3 ���");
                break;
            case "Item_4":
                interest.value += 10f;
                Debug.Log("������ 4 ���");
                break;
            case "Item_5":
                interest.value += 20f;
                Debug.Log("������ 5 ���");
                break;
            case "Item_6":
                interest.value += 30f;
                Debug.Log("������ 6 ���");
                break;
        }
    }

}
