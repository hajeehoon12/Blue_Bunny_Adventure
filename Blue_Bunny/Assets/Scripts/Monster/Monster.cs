using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monster Data / StateMachin 갖고있다
/// </summary>
public class Monster : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    [field: SerializeField] private MonsterData data;
    public MonsterData Data => data;
    public Animator Animator { get; private set; }
    public BoxCollider2D BoxCollider2D { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private MonsterStateMachine stateMachine;

    private void Awake()
    {
        AnimationData = new MonsterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new MonsterStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
