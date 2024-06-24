using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat : MonoBehaviour
{
    public float playerSpeed = 5;

    public float jumpPower = 15;

    public float bulletSpeed = 15;

    public float attackDamage = 1;

    public float playerHP = 100;

    public float playerMaxHP = 100;

    public float playerMP = 50;


    public float attackSpeed = 0.2f;


    private void Update()
    {
        playerMP += Time.deltaTime * 2;
    }

}
