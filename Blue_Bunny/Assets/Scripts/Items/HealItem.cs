using UnityEngine;

public class HealItem : Item, iHeal
{
    public float healValue = 10f;
    public void Heal()
    {
        CharacterManager.Instance.Player.battle.ChangeHealth(healValue);
        PlayHealSound();
    }
    public void PlayHealSound()
    {
        AudioManager.instance.PlaySFX("GetItem", 0.2f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            Heal();
        }
            base.OnTriggerEnter2D(collision);
    }

}

