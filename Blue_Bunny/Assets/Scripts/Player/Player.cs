using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
    public PlayerController controller;
    public PlayerStat stats;
    public PlayerBattle battle;
    public Pet pet;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStat>();
        battle = GetComponent<PlayerBattle>();
    }


    private void Start()
    {
        pet = GameObject.Find("PetLight").GetComponent<Pet>();
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;

        stats.playerSpeed = data.playerSpeed;
        stats.jumpPower = data.jumpPower;
        stats.bulletSpeed = data.bulletSpeed;
        stats.attackDamage = data.attackDamage;
        stats.playerHP = data.playerHP;
        stats.playerMaxHP = data.playerMaxHP;
        stats.playerMP = data.playerMP;
        stats.attackSpeed = data.attackSpeed;
        stats.playerGold = data.playerGold;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;

        data.playerSpeed = stats.playerSpeed;
        data.jumpPower = stats.jumpPower;
        data.bulletSpeed = stats.bulletSpeed;
        data.attackDamage = stats.attackDamage;
        data.playerHP = stats.playerHP;
        data.playerMaxHP = stats.playerMaxHP;
        data.playerMP = stats.playerMP;
        data.attackSpeed = stats.attackSpeed;
        data.playerGold = stats.playerGold;
   
        Debug.Log("Player Data Saved");
    }

}
