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
    public SpriteRenderer SpriteRenderer { get; private set; }

    private MonsterStateMachine stateMachine;

    public float Health { get; set; } = 3f;

    public event Action OnHealthChanged;

    public GameObject MonsterLife;

    private void Awake()
    {
        AnimationData = new MonsterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new MonsterStateMachine(this);
    }

    private void OnEnable()
    {
        stateMachine.Monster.BoxCollider2D.enabled = true;
        Health = Data.MaxHealth;
        OnHealthChanged?.Invoke();
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
            OnHealthChanged?.Invoke();
            /*Debug.Log($"Monster Health : {Health}");*/

            if (Health <= 0)
            {
                stateMachine.ChangeState(stateMachine.DeadState);
                Instantiate(MonsterLife, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity); 
            }
            else
            {
                stateMachine.ChangeState(stateMachine.GetHitState);
            }
        }
    }
}

