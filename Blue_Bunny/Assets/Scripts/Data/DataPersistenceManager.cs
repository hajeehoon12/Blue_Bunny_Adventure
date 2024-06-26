using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

/// <summary>
/// 데이터 저장 및 불러오기를 관리하는 클래스
/// 사용중인 DataManager 버전
/// GameData 클래스, json 파일 하나에 게임 데이터 다 넣는다
/// 10초마다 저장
/// 실제 데이터 저장은 FileDataHandler 클래스에서 한다
/// dataPersistenceObjects 에다가 IDataPersistence 인터페이스를 상속받은 클래스들을 찾아서 저장한다 -> SaveData, LoadData 호출
/// </summary>
public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool isNewGame = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 10f;
    private Coroutine autoSaveCoroutine;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }
    public bool IsNewGame { get { return isNewGame; } set { isNewGame = value; } }

    /// <summary>
    /// 싱글턴
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        Debug.Log(Application.persistentDataPath);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 씬 로드 되면 dataPersistenceObjects 다 찾아서 LoadGame 호출
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        if (isNewGame)
        {
            isNewGame = false;
            Debug.Log("Starting a new game.");
            NewGame();
            dataHandler.Save(gameData);
        }

        LoadGame();
        Debug.Log("Scene Loaded: " + scene.name);

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    /// <summary>
    /// 처음하기 -> 게임 데이터 초기값 넣어준대로 세팅
    /// </summary>
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    /// <summary>
    /// dataPersistenceObjects에 있는 각자 SaveData 호출
    /// 저장한 시간까지 바이너리로 추가해서 dataHandler에서 저장해주기
    /// </summary>
    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // timestamp the data so we know when it was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(gameData);

        Debug.Log("Game Saved!");
    }

    /// <summary>
    /// json 파일에서 데이터 불러오기
    /// 불러온 데이터 dataPersistenceObjects 객체들에 있는 각자의 LoadData 호출
    /// </summary>
    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. Starting a new game.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Game Loaded!");
    }

    /// <summary>
    /// IPeristenceData 인터페이스를 상속받은 클래스들을 찾아서 리스트로 반환
    /// 나중에 여기 있는 객체들에게 LoadData, SaveData 호출
    /// </summary>
    /// <returns></returns>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    /// <summary>
    /// 일정시간마다 자동으로 저장 호출
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}
