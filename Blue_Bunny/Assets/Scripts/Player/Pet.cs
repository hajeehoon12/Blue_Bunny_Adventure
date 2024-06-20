using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer _playerSprite;
    private ParticleSystem[] Flash;

    private void Awake()
    {
        Flash = GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        for(int i=0; i<Flash.Length; i++)
        {
            ParticleSystem.MainModule main = Flash[i].main;
            //main.startColor = Color.blue;
        }
    }

    private void Update()
    {
        float dir = _playerSprite.flipX ? 1 : -1;

        transform.LookAt(player.transform);
        transform.position = Vector3.Slerp(transform.position, player.transform.position + new Vector3(dir, 1), Time.deltaTime);
        

    }

}
