using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData data;

    public List<Transform> groundmonsterSpawnTr;
    public List<Transform> airMonsterSpawnTr;
    public Transform[] portalPos;

    public Transform rewardChestTr;
    public GameObject rewardChest;

    public bool isBossAlive = false;

    public Transform playerSpawnPos;

    public void SetRewardChestOn()
    {
        Instantiate(rewardChest, rewardChestTr);
    }
}
