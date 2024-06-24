using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// 처음 만들어질 때 데이터 초기화
    /// </summary>
    public GameData()
    {
        playerPosition = Vector3.zero;
        stageIdx = 0;

        playerSpeed = 5;
        jumpPower = 15;
        bulletSpeed = 15;
        attackDamage = 1;
        playerHP = 100;
        playerMaxHP = 100;
        playerMP = 50;
        attackSpeed = 0.2f;


    }
}
