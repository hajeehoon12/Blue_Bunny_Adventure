using UnityEngine;

public class SpeedUpItem : Item, iStatUpgrade
{
    public float speedUpgradeAmount = 1f;
    public void UpgradeStat()
    {
        CharacterManager.Instance.Player.stats.playerSpeed += speedUpgradeAmount;
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

