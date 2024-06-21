using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
    public SpriteRenderer spriteRenderer;

    private MonsterStateMachine stateMachine;

    public GameObject _monsterEffect;
    public float Health { get; set; } = 3f;

    public event Action OnHit;

    private void Awake()
    {
        AnimationData = new MonsterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new MonsterStateMachine(this);

        Health = Data.MaxHealth;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(Define.BULLET_TAG)) // When Hit by Bullet
        {
            Health--;

            Debug.Log($"Monster Health : {Health}");

            if (Health <= 0)
            {
                stateMachine.ChangeState(stateMachine.DeadState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.GetHitState);
                OnHit?.Invoke();
            }

        }
    }
}

