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

    public bool UseItem(ItemData item)
    {
        if (itemCounts.TryGetValue(item.itemName, out int count) && count > 0)
        {
            itemCounts[item.itemName]--;
            SaveData();
            return true;
        }
        else
        {
            Debug.Log($"{item.itemName} 아이템이 부족합니다.");
            return false;
        }
    }

    public void SaveData()
    {
        try
        {
            var saveData = new { discoveredItems, itemCounts };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
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

                // 키 존재 여부 체크
                if (saveData.ContainsKey("discoveredItems"))
                    discoveredItems = saveData["discoveredItems"].ToDictionary(kv => kv.Key, kv => Convert.ToBoolean(kv.Value));
                else
                    discoveredItems = new Dictionary<string, bool>();

                if (saveData.ContainsKey("itemCounts"))
                    itemCounts = saveData["itemCounts"].ToDictionary(kv => kv.Key, kv => Convert.ToInt32(kv.Value));
                else
                    itemCounts = new Dictionary<string, int>();

            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
                discoveredItems = new Dictionary<string, bool>();
                itemCounts = new Dictionary<string, int>();
            }
        }
        else
        {
            // 최초 실행 시 기본값 설정
            discoveredItems = new Dictionary<string, bool>();
            itemCounts = new Dictionary<string, int>();
            Debug.Log("저장 파일이 없어 초기화됨.");
        }
    }
}
