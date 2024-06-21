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

    public GameObject _monsterEffect;
    public float Health { get; set; } = 3f;

    public event Action OnHit;

    private void Awake()
    {
        AnimationData = new MonsterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new MonsterStateMachine(this);

        Health = Data.MaxHealth;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(Define.BULLET_TAG)) // When Hit by Bullet
        {
            Health--;

            Debug.Log($"Monster Health : {Health}");

            if (Health <= 0)
            {
                Dead();
            }
            else
            {
                stateMachine.ChangeState(stateMachine.GetHitState);
                OnHit?.Invoke();
            }

        }
    }

    public void Dead()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FadeOut());
        GameManager.Instance.spawnManager.ApplyAliveMonsterDeath();
    }

    IEnumerator FadeOut()
    {
        Instantiate(_monsterEffect, transform.position, Quaternion.identity);

        GetComponentInChildren<SpriteRenderer>().DOFade(0, 2f);
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().color += Color.black;
    }
}
