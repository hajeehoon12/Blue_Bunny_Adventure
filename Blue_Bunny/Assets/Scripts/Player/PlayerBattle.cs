using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBattle : MonoBehaviour
{


    public float healthChangeDelay = 0.5f;
    

    private float timeSinceLastChange = float.MaxValue; // time calculate from last hit

    public bool isAttacked = false;


    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;


    public float MaxHealth => CharacterManager.Instance.Player.stats.playerMaxHP;

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                //isAttacked = false;

            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay) // if not attacked
        {
            return false;
        }






        CharacterManager.Instance.Player.stats.playerHP += change; // health change value
        CharacterManager.Instance.Player.stats.playerHP = Mathf.Clamp(CharacterManager.Instance.Player.stats.playerHP, 0, CharacterManager.Instance.Player.stats.playerMaxHP); // restrict health range for 0<= health <= maxHealth

        //Debug.Log("Player Health : " + change);

        if (CharacterManager.Instance.Player.stats.playerHP <= 0f)
        {
            timeSinceLastChange = 0f;
            Debug.Log("Player Dead");
            CallDeath();
            return true;
        }
        if (change >= 0) // when change is positive = Healing character
        {
            OnHeal?.Invoke();
        }
        else // When Damage to Player
        {
            //Debug.Log("Damage Motion called");
            OnDamage?.Invoke();
            timeSinceLastChange = 0f;
            

            // Get Damaged Sound
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

}
