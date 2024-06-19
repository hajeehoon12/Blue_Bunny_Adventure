using UnityEngine;

[SerializeField]
public class MonsterData
{
    [SerializeField] public float ChasingRange { get; private set; } = 10.0f;
    [SerializeField] public float BaseSpeed { get; private set; } = 5.0f;
    [SerializeField] public float AttackRange { get; private set; } = 3.0f;
}