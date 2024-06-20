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

    public void SpawnMonster()
    {
        spawnCount = nowMap.data.spawnCount;
        aliveMonsterCount = spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monster = PoolManager.Instance.Get(2);
            //랜덤한 위치에 생성 후 리스트에서 제거. (동일 위치 생성 방지)
            int rdSpawnIdx = Random.Range(0, nowMap.monsterSpawnTr.Count);

            Transform spawnPos = nowMap.monsterSpawnTr[rdSpawnIdx];
            monster.transform.position = spawnPos.position;
            nowMap.monsterSpawnTr.Remove(spawnPos);
        }
    }

    public void SpawnPortal()
    {

    }
}
