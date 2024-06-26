using System;
using UnityEngine;

/// <summary>
/// 상하 or 좌우 움직이는 타입
/// </summary>
public enum MonsterType
{
    Horizontal,
    Vertical,
}

/// <summary>
/// Monster의 데이터를 관리하는 클래스
/// </summary>
[Serializable]
public class MonsterData
{
    [SerializeField] public MonsterType MonsterType = MonsterType.Horizontal;

    [SerializeField] public float ChasingRange = 5.0f;
    [SerializeField] public float ChasingSpeed  = 5.0f;
    [SerializeField] public float IdleSpeed = 1.0f;
    [SerializeField] public float IdleChangeDirectionSecond = 1.0f;
    [SerializeField] public float AttackDamage = 1.0f;
    [SerializeField] public float MaxHealth = 20.0f;
    [SerializeField] public float IdleFlyDistance = 7.0f;
}