using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_MaxHPUpItem : StoreItem, iStatUpgrade
{
    public float maxHPUpgradeAmount = 10f;

    protected override void Buy()
    {
        if (CharacterManager.Instance.Player.stats.playerGold > itemData.cost)
        {
            CharacterManager.Instance.Player.stats.playerGold -= itemData.cost;
            PlayUpgradeSound();
            GotoInventoryTab();
            UpgradeStat();

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

    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.playerMaxHP += maxHPUpgradeAmount;
        CharacterManager.Instance.Player.battle.ChangeHealth(0);
        UIManager.Instance.Condition.HpBar.ChangeText();
    }
}
