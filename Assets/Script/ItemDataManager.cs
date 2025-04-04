using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;

public class ItemDataManager : MonoBehaviour
{
    private static ItemDataManager _instance;
    public static ItemDataManager Instance { get; private set; }

    private Dictionary<string, bool> discoveredItems = new Dictionary<string, bool>();
    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

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

        filePath = Application.persistentDataPath + "/saveData.json";
        LoadData();
    }

    public bool IsItemDiscovered(ItemData item)
    {
        return discoveredItems.TryGetValue(item.itemName, out bool isDiscovered) && isDiscovered;
    }

    public void DiscoverItem(ItemData item)
    {
        if (!discoveredItems.ContainsKey(item.itemName))
        {
            discoveredItems[item.itemName] = true;
            Debug.Log($"{item.itemName} 습득! 도감에 등록되었습니다.");
        }else
        {
            IsItemDiscovered(item);
        }
    }

    public int GetItemCount(ItemData item)
    {
        return itemCounts.TryGetValue(item.itemName, out int count) ? count : 0;
    }

    public void AddItem(ItemData item, int amount)
    {
        if (!itemCounts.ContainsKey(item.itemName))
        {
            itemCounts[item.itemName] = 0;
        }
        DiscoverItem(item);
        itemCounts[item.itemName] += amount;
        Debug.Log(item.itemName + " " + GetItemCount(item));
        SaveData();
    }

    public void UseItem(ItemData item)
    {
        if (itemCounts.TryGetValue(item.itemName, out int count) && count > 0)
        {
            itemCounts[item.itemName]--;
            SaveData();
        }
        else
        {
            Debug.Log($"{item.itemName} 아이템이 부족합니다.");
        }
    }

    public void SaveData()
    {
        try
        {
            var saveData = new { discoveredItems, itemCounts };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
            Debug.Log("데이터 저장 완료: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("데이터 저장 오류: " + e.Message);
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

                discoveredItems = saveData["discoveredItems"].ToDictionary(kv => kv.Key, kv => (bool)kv.Value);
                itemCounts = saveData["itemCounts"].ToDictionary(kv => kv.Key, kv => Convert.ToInt32(kv.Value));
                Debug.Log("데이터 로드 완료!");
            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
            }
        }
    }
}
