using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_AtkSpeedUPItem : StoreItem, iStatUpgrade
{

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
        CharacterManager.Instance.Player.stats.attackDamage += 1f;
        CharacterManager.Instance.Player.stats.playerSpeed += 1f;
        CharacterManager.Instance.Player.battle.ChangeHealth(0);
        UIManager.Instance.Condition.HpBar.ChangeText();
    }
}
