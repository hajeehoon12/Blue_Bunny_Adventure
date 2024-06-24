using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MonoBehaviour
{
    public Transform[] bossItemSpawnTr;
    public GameObject[] bossItemPrefabs;

    private List<GameObject> nowMapItemList = new List<GameObject>();

    public bool isSelectAnyItem = false;

    public void SpawnBossRewardItem()
    {
        for(int i = 0; i < bossItemSpawnTr.Length; i++)
        {
            GameObject go = Instantiate(bossItemPrefabs[i], bossItemSpawnTr[i]);
            nowMapItemList.Add(go);
        }
    }

    public void RemoveUnSelectedBossItem()
    {
        for(int i = 0; i < nowMapItemList.Count; i++)
        {
            Destroy(nowMapItemList[i]); 
        }
    }
}
