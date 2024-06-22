using UnityEngine;

public class HealItem : Item, iHeal
{
    public float healValue = 10f;
    public void Heal()
    {
        CharacterManager.Instance.Player.stats.playerHP += healValue;
        Debug.Log("체력 회복");
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

