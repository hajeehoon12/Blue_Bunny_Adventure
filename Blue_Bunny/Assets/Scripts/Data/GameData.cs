using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 playerPosition;

    public GameData()
    {
        playerPosition = Vector3.zero;
    }
}
