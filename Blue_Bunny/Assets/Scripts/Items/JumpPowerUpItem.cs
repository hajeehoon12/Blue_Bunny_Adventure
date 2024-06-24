using UnityEngine;

public class JumpPowerUpItem : Item, iStatUpgrade
{
    public float jumpPowerUpgradeAmount = 1f;
    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.jumpPower += jumpPowerUpgradeAmount;
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
        if (!UIManager.Instance.Item.IsFull() && collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            UpgradeStat();
            GotoInventoryTab();
        }
            base.OnTriggerEnter2D(collision);
    }
}

