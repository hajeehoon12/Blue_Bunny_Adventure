using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_FullHPSpeedUPItem : StoreItem, iHeal, iStatUpgrade
{
    public float speedUpgradeAmount = 1f;

    protected override void Buy()
    {
        // 플레이어의 돈이 아이템 가격보다 많다면
        PlayUpgradeSound();
        GotoInventoryTab();
        UpgradeStat();
        Heal();
    }

    public void GotoInventoryTab()
    {
        UIManager.Instance.Item.AddItem(itemData);
    }

    public void PlayUpgradeSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }

    public void Heal()
    {
        CharacterManager.Instance._player.stats.playerHP = CharacterManager.Instance._player.stats.playerMaxHP;
    }

    public void PlayHealSound()
    {
        // 힐 소리 있으면 따로 재생
    }

    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.playerSpeed += speedUpgradeAmount;
    }

}

