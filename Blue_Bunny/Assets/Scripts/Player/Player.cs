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
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }

}
