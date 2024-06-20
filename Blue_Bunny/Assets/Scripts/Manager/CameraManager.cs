using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform _player;
    public MeshRenderer render;

    private Vector2 _firstPos;

    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    float screenHeight;
    float screenWidth;

    private void Start()
    {
        _firstPos = _player.position;

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Screen.width / Screen.height;
    }

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 3f, -10);
        render.material.mainTextureOffset = new Vector2((_firstPos.x - _player.position.x) / 100, 0);

        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        float borderx = mapSize.x - screenWidth;
        float clampX = Mathf.Clamp(transform.position.x, -borderx + center.x, borderx + center.x);

        float bordery = mapSize.y - screenHeight;
        float clampY = Mathf.Clamp(transform.position.y, -bordery + center.y, bordery + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
