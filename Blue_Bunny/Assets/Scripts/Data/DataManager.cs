using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    string persistentDataPath;

    public StartData Start { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        persistentDataPath = Application.persistentDataPath;
    }

    public void SaveData<T>(T json)
    {
        File.WriteAllText(persistentDataPath + $"/{typeof(T).ToString()}.json", JsonUtility.ToJson(json));
        Debug.Log($"DataManager::SaveData : {typeof(T).ToString()}.json saved.");
    }

    public T LoadData<T>()
    {
        if(!File.Exists(persistentDataPath + $"/{typeof(T).ToString()}.json"))
        {
            Debug.Log($"DataManager::LoadData : {typeof(T).ToString()}.json not found.");
            return JsonUtility.FromJson<T>(null);
        }

        string jsonData = File.ReadAllText(persistentDataPath + $"/{typeof(T).ToString()}.json");
        Debug.Log($"DataManager::LoadData : {typeof(T).ToString()}.json loaded.");
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void Init()
    {
        Start = new StartData();
    }

    public void LoadGame()
    {
        Start = LoadData<StartData>();
    }

    // TODO : SO 데이터 불러오기

}