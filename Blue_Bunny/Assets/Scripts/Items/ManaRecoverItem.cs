using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRecoverItem : Item, iHeal
{
    public float healValue = 10f;
    public void Heal()
    {
        CharacterManager.Instance.Player.stats.playerMP += healValue;
        Debug.Log("마나 회복");
        PlayHealSound();
    }
    public void PlayHealSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Heal();
        base.OnTriggerEnter2D(collision);
    }
}
