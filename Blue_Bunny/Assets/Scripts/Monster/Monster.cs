using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// Monster 전체적인 행동을 관리하는 클래스
/// 갖고있는 statemachine에 있는 라이프사이클 함수 돌리기
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

    public MonsterStateMachine stateMachine;

    public float Health { get; set; } = 20f;

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

            

            Health -= CharacterManager.Instance.Player.stats.attackDamage;
            OnHealthChanged?.Invoke();
            /*Debug.Log($"Monster Health : {Health}");*/
            AudioManager.instance.PlayPitchSFX("MonsterGetHit", 0.2f);

            if (Health <= 0)
            {
                stateMachine.ChangeState(stateMachine.DeadState);
                GameObject lifeLight = Instantiate(MonsterLife, CharacterManager.Instance.Player.transform.position, Quaternion.identity); 
                lifeLight.transform.position = transform.position;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.GetHitState);
            }
        }
    }
}

