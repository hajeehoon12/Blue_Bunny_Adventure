using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public SpawnManager spawnManager;

    public GameObject[] mapPrefabs;
    public int stageIdx = 0;

    private void Awake()
    {
        if (_instance != null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        ChangeMap();
    }

    public void ChangeMap()
    {
        GameObject go = Instantiate(mapPrefabs[stageIdx]);
        
        spawnManager.nowMap = go.GetComponent<Map>();
        spawnManager.SpawnMonster();
    }
}
