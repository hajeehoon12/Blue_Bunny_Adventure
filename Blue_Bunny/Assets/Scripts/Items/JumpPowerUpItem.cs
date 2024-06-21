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
        //인벤토리 탭에 표시되는 기능 구현하기.
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        UpgradeStat();
        base.OnTriggerEnter2D(collision);
    }
}

