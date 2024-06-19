using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform _player;


    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _player.transform.position.y + 2.5f, -10);
    }
}
