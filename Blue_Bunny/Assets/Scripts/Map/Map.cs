using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData data;

    public List<Transform> monsterSpawnTr;
    public Transform portalPos;

    public Transform rewardChestTr;
    public GameObject rewardChest;

    public bool isCleared = false;

    public void SetRewardChestOn()
    {
        Instantiate(rewardChest, rewardChestTr);
    }
}
