using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform _player;
    public MeshRenderer render;

    private Vector2 _firstPos;

    private void Start()
    {
        _firstPos = _player.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _player.transform.position.y + 3f, -10);
        render.material.mainTextureOffset = new Vector2((_firstPos.x - _player.position.x)/100, 0);
    }
}
