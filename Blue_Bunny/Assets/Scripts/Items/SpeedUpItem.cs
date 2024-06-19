using UnityEngine;

public class SpeedUpItem : Item, iStatUpgrade
{
    public void UpgradeStat()
    {
        Debug.Log("속도 증가!");
        PlayUpgradeSound();
    }
    public void PlayUpgradeSound()
    {
        Debug.Log("스피드 업 소리 재생!");
        //AudioManager.instance.PlayBGM("");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        UpgradeStat();
        base.OnTriggerEnter2D(collision);
    }
}

