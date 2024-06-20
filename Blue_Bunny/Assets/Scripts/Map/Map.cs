using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData data;

    public List<Transform> monsterSpawnTr;
    public bool isCleared = false;

    public GameObject portal;

    public void UsePortal()
    {
        isCleared = true;
    }
}
