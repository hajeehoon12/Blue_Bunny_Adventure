using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 저장 필요한 데이터 모음
/// json 파일 하나에 다 넣어서 저장한다
/// </summary>
[Serializable]
public class GameData
{
    // Save Time
    public long lastUpdated;

    // Player
    public Vector3 playerPosition;

    // GameManager
    public int stageIdx;

    // PlayerStat
    public float playerSpeed;
    public float jumpPower;
    public float bulletSpeed;
    public float attackDamage;
    public float playerHP;
    public float playerMaxHP;
    public float playerMP;
    public float attackSpeed;
    public float playerGold;

    // SpawnManager
    public int killedGroundCount;
    public int killedAirCount;

    // ItemUI
    public List<int> ItemsData;

    /// <summary>
    /// 처음 만들어질 때 데이터 초기화
    /// </summary>
    public GameData()
    {
        playerPosition = Vector3.zero;
        stageIdx = -1;

        playerSpeed = 5;
        jumpPower = 15;
        bulletSpeed = 15;
        attackDamage = 5;
        playerHP = 100;
        playerMaxHP = 100;
        playerMP = 50;
        attackSpeed = 0.2f;
        playerGold = 0f;

        killedGroundCount = 0;
        killedAirCount = 0;

        ItemsData = new List<int>();
    }
}
