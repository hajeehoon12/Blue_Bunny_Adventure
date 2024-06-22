using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_MaxHPUpItem : StoreItem, iStatUpgrade
{
    public float maxHPUpgradeAmount = 10f;

    protected override void Buy()
    {
        // 플레이어의 돈이 아이템 가격보다 많다면
        PlayUpgradeSound();
        GotoInventoryTab();
        UpgradeStat();
    }

    public void GotoInventoryTab()
    {
        UIManager.Instance.Item.AddItem(itemData);
    }

    public void PlayUpgradeSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }

    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.playerMaxHP += maxHPUpgradeAmount;
    }
}
