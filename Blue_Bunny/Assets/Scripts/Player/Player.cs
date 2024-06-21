using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerStat stats;
    public PlayerBattle battle;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStat>();
        battle = GetComponent<PlayerBattle>();
    }

}
