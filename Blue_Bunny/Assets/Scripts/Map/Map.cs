using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Transform> monsterSpawnTr;
    public bool isCleared = false;

    public GameObject portal;

    public void UsePortal()
    {
        isCleared = true;
    }
}
