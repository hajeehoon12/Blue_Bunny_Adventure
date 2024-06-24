using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //맵 인덱스를 가져와서 해당 맵에 맞는 스폰 포인트를 가져온 뒤 PoolManager.Instance.Get(번호)로 오브젝트를 가져온다.
    public int mapIndex;

    public Map nowMap;

    public int spawnCount;
    public int aliveMonsterCount = 0;

    public GameObject portalPrefab;
    public GameObject[] itemPrefabs;
    public GameObject storePortal;

    public void SpawnMonstertoMap()
    {
        spawnCount = nowMap.data.ground_spawnCount + nowMap.data.air_spawnCount;
        aliveMonsterCount = spawnCount;

        if (nowMap.data.isBossStage) // not 0 and after 3stages
        {
            nowMap.isBossAlive = true; 
            GameObject boss = PoolManager.Instance.Get(3);
            // 보스 타입을 air인지 ground 인지 정하면 좋을듯.
            Transform bossSpawnPos = nowMap.airMonsterSpawnTr[0];
            boss.transform.position = bossSpawnPos.position;
            return;
        }

        for (int i = 0; i < nowMap.data.ground_spawnCount; i++)
        {
            SpawnMonster(2, true);
        }

        for(int i = 0; i < nowMap.data.air_spawnCount; i++)
        {
            int randomIdx = Random.Range(4, 6);
            SpawnMonster(randomIdx, false);
        }
    }

    public void ApplyAliveMonsterDeath()
    {
        aliveMonsterCount--;
        if(aliveMonsterCount == 0 && !nowMap.isBossAlive)
        {           
            SpawnPortal();
            SpawnRewardChest();
        }
    }

    public void SpawnMonster(int index, bool isGroundMonster)
    {
        GameObject monster = PoolManager.Instance.Get(index);
        //랜덤한 위치에 생성 후 리스트에서 제거. (동일 위치 생성 방지)
        if (isGroundMonster)
        {
            int rdSpawnIdx = Random.Range(0, nowMap.groundmonsterSpawnTr.Count);

            Transform spawnPos = nowMap.groundmonsterSpawnTr[rdSpawnIdx];
            monster.transform.position = spawnPos.position;
            nowMap.groundmonsterSpawnTr.Remove(spawnPos);
        }

        else
        {
            int rdSpawnIdx = Random.Range(0, nowMap.airMonsterSpawnTr.Count);

            Transform spawnPos = nowMap.airMonsterSpawnTr[rdSpawnIdx];
            monster.transform.position = spawnPos.position;
            nowMap.airMonsterSpawnTr.Remove(spawnPos);
        }
    }
    public void SpawnPortal()
    {
        Instantiate(portalPrefab, nowMap.portalPos[0]);
        if(nowMap.data.mapIndex == 2)
            Instantiate(storePortal, nowMap.portalPos[1]);
        Debug.Log("다음 스테이지로 갈 수 있는 포탈이 생성되었습니다!");
    }

    public void SpawnRewardChest()
    {
        nowMap.SetRewardChestOn();
        Debug.Log("보상이 들어있는 상자가 등장했습니다!");
    }

    public void SpawnBoxRewardItem(Transform spawnTr)
    {
        int rdIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[rdIndex], spawnTr);
    }
}
