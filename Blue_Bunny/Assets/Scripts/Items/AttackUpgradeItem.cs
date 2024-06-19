using UnityEngine;

public class AttackUpgradeItem : Item, iStatUpgrade
{
    public void UpgradeStat()
    {
        Debug.Log("공격력이 강해졌다!");
        PlayUpgradeSound();
    }

    public void PlayUpgradeSound()
    {
        Debug.Log("공격력 증가 소리 재생!");
        //AudioManager.instance.PlayBGM("");
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

