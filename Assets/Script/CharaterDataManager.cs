using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;

public class CharaterDataManager : MonoBehaviour
{
    private static CharaterDataManager _instance;
    public static CharaterDataManager Instance { get; private set; }

    private Dictionary<string, bool> discoveredCharater = new Dictionary<string, bool>();
    private Dictionary<string, int> charaterCount = new Dictionary<string, int>(); 

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Application.persistentDataPath + "/charaterSaveData.json";
        LoadData();
    }

    public bool IsCharaterDiscovered(CharatorData charater)
    {
        return discoveredCharater.TryGetValue(charater.name, out bool isDiscovered) && isDiscovered;
    }

    public void DiscoverCharater(CharatorData charater)
    {
        if (!discoveredCharater.ContainsKey(charater.name))
        {
            discoveredCharater[charater.name] = true;
            Debug.Log($"{charater.name} ����! ������ ��ϵǾ����ϴ�.");
        }
        else
        {
            IsCharaterDiscovered(charater);
        }
    }

    public void AddCharater(CharatorData charater)
    {
        if(!charaterCount.ContainsKey(charater.name))
        {
            charaterCount[charater.name] = 0;
        }
        DiscoverCharater(charater);
        charaterCount[charater.name] ++;
        Debug.Log(charater.name + " ĳ���� " + charaterCount[charater.name] + " ��° ŉ��");
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            var saveData = new { discoveredCharater, charaterCount };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
            Debug.Log("������ ���� �Ϸ�: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("������ ���� ����: " + e.Message);
        }
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);

                // Ű ���� ���� üũ
                if (saveData.ContainsKey("discoveredCharater"))
                    discoveredCharater = saveData["discoveredCharater"].ToDictionary(kv => kv.Key, kv => Convert.ToBoolean(kv.Value));
                else
                    discoveredCharater = new Dictionary<string, bool>();

                if (saveData.ContainsKey("charaterCount"))
                    charaterCount = saveData["charaterCount"].ToDictionary(kv => kv.Key, kv => Convert.ToInt32(kv.Value));
                else
                    charaterCount = new Dictionary<string, int>();


                Debug.Log("ĳ���� ������ �ε� �Ϸ�!");
            }
            catch (Exception e)
            {
                Debug.LogError("������ �ε� ����: " + e.Message);
                discoveredCharater = new Dictionary<string, bool>();
            }
        }
        else
        {
            // ���� ���� �� �⺻�� ����
            discoveredCharater = new Dictionary<string, bool>();
            Debug.Log("���� ������ ���� �ʱ�ȭ��.");
        }
    }
}
