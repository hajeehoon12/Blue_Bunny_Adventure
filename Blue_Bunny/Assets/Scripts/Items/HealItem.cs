using UnityEngine;

public class HealItem : Item, iHeal
{
    public void Heal()
    {
        Debug.Log("체력이 회복되었다!");
    }
    public void PlayHealSound()
    {
        Debug.Log("체력 회복 소리 재생!");
        //AudioManager.instance.PlayBGM("");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Heal();
        base.OnTriggerEnter2D(collision);
    }

}

