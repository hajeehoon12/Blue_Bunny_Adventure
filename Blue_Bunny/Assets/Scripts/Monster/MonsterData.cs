using UnityEngine;

public enum MonsterType
{
    Horizontal,
    Vertical,
}

[SerializeField]
public class MonsterData
{
    [SerializeField] public float ChasingRange { get; private set; } = 5.0f;
    [SerializeField] public float ChasingSpeed { get; private set; } = 5.0f;
    [SerializeField] public float IdleSpeed { get; private set; } = 1.0f;
    [SerializeField] public MonsterType MonsterType { get; private set; } = MonsterType.Horizontal;
    [SerializeField] public float IdleChangeDirectionSecond { get; private set; } = 1.0f;

}