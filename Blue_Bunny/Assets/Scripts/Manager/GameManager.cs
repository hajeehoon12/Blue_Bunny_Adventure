using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SpawnManager spawnManager;

    public GameObject[] mapPrefabs;

    public GameObject storeMapPrefab;
    private GameObject storeMapObject;

    private bool storeMapCreated = false;
    public bool isInStore = false;

    // 상점 진입 전 플레이어 위치
    private Vector2 playerPrePos;

    public int stageIdx = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeMap();
    }

    public void ChangeMap()
    {
        GameObject go = Instantiate(mapPrefabs[stageIdx]);
        
        if(spawnManager.nowMap != null)
        {
            Destroy(spawnManager.nowMap.gameObject);
        }
        spawnManager.nowMap = go.GetComponent<Map>();
        spawnManager.SpawnMonster();
        stageIdx++;
    }

    public void GotoStoreMap()
    {
        playerPrePos = CharacterManager.Instance._player.transform.position;
        spawnManager.nowMap.gameObject.SetActive(false);
        isInStore = true;
        if (!storeMapCreated)
        {
            storeMapObject = Instantiate(storeMapPrefab);
            storeMapCreated = true;
            return;
        }

        storeMapObject.GetComponent<StoreMap>().AdjustPlayerPosition();
        Debug.Log(spawnManager.nowMap.gameObject);
        storeMapObject.SetActive(true);
    }

    public void ReturnToMap()
    {
        isInStore = false;
        storeMapObject.SetActive(false);
        spawnManager.nowMap.gameObject.SetActive(true);
        CharacterManager.Instance._player.transform.position = playerPrePos - (Vector2.left * 2);
        spawnManager.nowMap.GetComponentInChildren<FloorToCameraData>().InitailizeMapCamera();
    }
}
