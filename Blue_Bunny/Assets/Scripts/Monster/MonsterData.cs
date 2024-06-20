using System;
using UnityEngine;

public enum MonsterType
{
    Horizontal,
    Vertical,
}

[Serializable]
public class MonsterData
{
    [SerializeField] public MonsterType MonsterType = MonsterType.Horizontal;

    [SerializeField] public float ChasingRange = 5.0f;
    [SerializeField] public float ChasingSpeed  = 5.0f;
    [SerializeField] public float IdleSpeed = 1.0f;
    [SerializeField] public float IdleChangeDirectionSecond = 1.0f;
    [SerializeField] public float AttackDamage = 1.0f;
}