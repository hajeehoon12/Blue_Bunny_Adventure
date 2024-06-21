using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SpawnManager spawnManager;

    public GameObject[] mapPrefabs;

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
}
