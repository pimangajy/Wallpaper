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
            Debug.Log($"{charater.name} 습득! 도감에 등록되었습니다.");
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
        Debug.Log(charater.name + " 캐릭터 " + charaterCount[charater.name] + " 번째 흭득");
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            var saveData = new { discoveredCharater, charaterCount };
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

                // 키 존재 여부 체크
                if (saveData.ContainsKey("discoveredCharater"))
                    discoveredCharater = saveData["discoveredCharater"].ToDictionary(kv => kv.Key, kv => Convert.ToBoolean(kv.Value));
                else
                    discoveredCharater = new Dictionary<string, bool>();

                if (saveData.ContainsKey("charaterCount"))
                    charaterCount = saveData["charaterCount"].ToDictionary(kv => kv.Key, kv => Convert.ToInt32(kv.Value));
                else
                    charaterCount = new Dictionary<string, int>();


                Debug.Log("캐릭터 데이터 로드 완료!");
            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
                discoveredCharater = new Dictionary<string, bool>();
            }
        }
        else
        {
            // 최초 실행 시 기본값 설정
            discoveredCharater = new Dictionary<string, bool>();
            Debug.Log("저장 파일이 없어 초기화됨.");
        }
    }
}
