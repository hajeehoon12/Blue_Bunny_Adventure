using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public float tempHealth = 5f;

    private void Awake()
    {
        AnimationData = new MonsterAnimationData();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(Define.BULLET_TAG)) // When Hit by Bullet
        {
            tempHealth--;

            Debug.Log($"Monster Health : {tempHealth}");

            if (tempHealth <= 0)
            {
                Dead();
            }
            else
            {
                stateMachine.ChangeState(stateMachine.GetHitState);
            }

        }
    }

    public void Dead()
    {
        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(FadeOut());
        
    }

    IEnumerator FadeOut()
    {
        Instantiate(_monsterEffect, transform.position, Quaternion.identity);

        spriteRenderer.DOFade(0, 1f);
        yield return new WaitForSeconds(2.5f);
        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.DOFade(1, 1f);
        gameObject.SetActive(false);
        
        GameManager.Instance.spawnManager.ApplyAliveMonsterDeath();
    }
}
