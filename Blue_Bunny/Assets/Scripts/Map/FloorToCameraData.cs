using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorToCameraData : MonoBehaviour
{
    Collider2D tileMap;


    private void Awake()
    {
        tileMap = GetComponent<Collider2D>();
    }


    private void Start()
    {
        CameraManager.Instance.mapSize = tileMap.bounds.extents + new Vector3(0,2,0);
    }



}
