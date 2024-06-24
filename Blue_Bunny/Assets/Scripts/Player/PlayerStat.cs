using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float playerSpeed = 5;

    public float jumpPower = 15;

    public float bulletSpeed;

    public float attackDamage;

    public float playerHP;

    public float playerMaxHP;

    public float playerMP;

    public float attackSpeed;

    private void Update()
    {
        playerMP += Time.deltaTime * 2;
    }
}
