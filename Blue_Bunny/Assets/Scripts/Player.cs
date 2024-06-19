using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;


    private void Awake()
    {
        //CharacterManager.Instance.Monster = this;
        controller = GetComponent<PlayerController>();
    }

}
