using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StartData
{
    public Vector3 playerPosition;

    public StartData()
    {
        playerPosition = Vector3.zero;
    }
}
