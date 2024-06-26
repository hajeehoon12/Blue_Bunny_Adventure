using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// WorldSpace 공간에서 몬스터 머리 위 체력 바 UI
/// </summary>
public class UIWorldSpace_MonsterHPBar : MonoBehaviour
{

    private Slider slider;
    private Monster monster;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        monster = GetComponentInParent<Monster>();

        monster.OnHealthChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        slider.value = monster.Health / monster.Data.MaxHealth;
    }
}
