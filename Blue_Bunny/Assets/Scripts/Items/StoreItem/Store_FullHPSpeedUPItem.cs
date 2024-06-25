using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_FullHPSpeedUPItem : StoreItem, iHeal, iStatUpgrade
{
    public float speedUpgradeAmount = 1f;

    protected override void Buy()
    {
        if (CharacterManager.Instance.Player.stats.playerGold > itemData.cost)
        {
            CharacterManager.Instance.Player.stats.playerGold -= itemData.cost;
            PlayUpgradeSound();
            GotoInventoryTab();
            UpgradeStat();
            Heal();

            Destroy(gameObject);
        }
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
        CharacterManager.Instance.Player.battle.ChangeHealth(CharacterManager.Instance.Player.stats.playerMaxHP);
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

