using UnityEngine;

public class AttackUpgradeItem : Item, iStatUpgrade
{
    public float attackUpgradeAmount = 1f;

    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.attackDamage += attackUpgradeAmount;
        PlayUpgradeSound();
    }

    public void PlayUpgradeSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }

    public void GotoInventoryTab()
    {
        UIManager.Instance.Item.AddItem(itemData);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        UpgradeStat();
        GotoInventoryTab();
        base.OnTriggerEnter2D(collision);
    }
}

