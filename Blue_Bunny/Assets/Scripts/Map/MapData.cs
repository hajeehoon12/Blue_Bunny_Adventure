using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "New MapData")]
public class MapData : ScriptableObject
{
    public int mapIndex;
    public int spawnCount;
}
