using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoreMap : MonoBehaviour
{
    public GameObject[] storeItems;

    public Transform[] itemSpawnTr;

    public Transform playerSpawnPos;

    private void Awake()
    {
        SpawnItem();
    }

    private void Start()
    {
        AdjustPlayerPosition();
    }

    private void SpawnItem()
    {
        foreach(var spawnTr in itemSpawnTr)
        {
            int rdIndex = Random.Range(0, storeItems.Length);
            Instantiate(storeItems[rdIndex], spawnTr);
        }      
    }

    // 상점 맵 이동 시 플레이어의 위치는 특정 지점이다.
    public void AdjustPlayerPosition()
    {
        CharacterManager.Instance._player.transform.position = playerSpawnPos.position;
    }
}
