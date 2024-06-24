using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 playerPosition;
    public PlayerStat playerStat;
    public int stageIdx;

    public GameData()
    {
        playerPosition = Vector3.zero;
        playerStat = new PlayerStat();
        stageIdx = 0;
    }
}
