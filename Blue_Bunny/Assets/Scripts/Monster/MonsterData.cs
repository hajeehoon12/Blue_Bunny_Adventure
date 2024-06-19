using UnityEngine;

[SerializeField]
public class MonsterData
{
    [SerializeField] public float ChasingRange { get; private set; } = 5.0f;
    [SerializeField] public float BaseSpeed { get; private set; } = 5.0f;
    [SerializeField] public float IdleMovingRange { get; private set; } = 2.0f;
}