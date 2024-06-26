using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 사용 X
/// 타입별로 json 파일 만들어서 저장하고 불러오는 클래스
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    string persistentDataPath;

    public GameData Start { get; private set; }

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
        Start = new GameData();
    }

    public void LoadGame()
    {
        Start = LoadData<GameData>();
    }

    // TODO : SO 데이터 불러오기

}