using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BossController : MonoBehaviour
{

    private static readonly int isRunning = Animator.StringToHash("IsRunning");
    private static readonly int isJumping = Animator.StringToHash("IsJumping");
    private static readonly int isFalling = Animator.StringToHash("IsFalling");
    private static readonly int isRolling = Animator.StringToHash("IsRolling");
    private static readonly int isAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int isDashing = Animator.StringToHash("IsDashing");
    private static readonly int isWallClimbing = Animator.StringToHash("IsWallClimbing");


    Animator animator;

    public float maxSpeed;// 최대속도 설정
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    GhostDash ghostDash;
    Collider2D playerCollider;

    bool Jumping = false;           // AM i Jumping?
    //bool Falling = false;
    bool Rolling = false;           // AM i rolling?
    public bool isGrounded = true;  // AM i on the ground?
    bool canCombo = false;          // AM i doing combo attack

    private float playerGravityScale;
    
    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;

    public bool canRoll = true;           // skill on / off
    public bool canDash = true;           // skill on / off
    public bool canComboAttack = true;    // skill on / off

    Vector2 boundPlayer;


    public float attackRate = 10f;  //attack Damage
    public int ComboCount;          // current combo Count

    public Transform _player;

    Vector2 diff;
    private float diffDist;

    private float dashInterval = 1f;
    private float dashCoolTime = 0f;
    private float rollInterval = 1.5f;
    private float rollCoolTime = 0f;

    private bool canDoDown = true;

    public bool isBossDie = false;
    public float bossMaxHP = 5f;
    public float bossHP;



    void Awake()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ghostDash = GetComponent<GhostDash>();
        playerCollider = GetComponent<Collider2D>();
        
    }

    private void Start()
    {
        playerGravityScale = rigid.gravityScale;
        boundPlayer = playerCollider.bounds.extents;
        _player = CharacterManager.Instance.Player.transform;

        bossHP = bossMaxHP;

    }

    private void FixedUpdate()
    {
        if (isBossDie) return;
        Move();
    }
    void Update()
    {
        if (isBossDie) return;
        //OnDash()
        //OnRoll();
        JumpCheck(); // Checking wheter can jump
        DoAction();
        //OnAttack();
        //OnClick();
        

    }

    void DoAction()
    {

        diff = (transform.position - _player.transform.position);

        


        diffDist = (Vector3.Distance(_player.transform.position, transform.position));

        if (diff.x < 3 && diff.x > -3)
        {
            if (diff.y < -0.7f)
            {
                TryJump();
                return;
            }
            else if (diff.y > 0.7f)
            {
                TryDown();
                return;
            }
        }


        if (diffDist < 2f)
        {
            if (diff.y < -2f)
            {
                TryJump();
                return;
            }
            else if (diff.y > 2f)
            {
                TryDown();
                return;
            }

            OnAttack();
            return;
        }

        if (diffDist < 7f)
        {
            if (dashCoolTime > dashInterval)
            {
                dashCoolTime -= dashInterval;
                OnDash();
                return;
            }
            else
            {
                dashCoolTime += Time.deltaTime;
            }
        }

        if (rollCoolTime > dashCoolTime)
        {
            rollCoolTime -= rollInterval;
            OnRoll();
            return;
        }
        else
        { 
            rollCoolTime += Time.deltaTime;
        }

        if (diff.y > 0.7f)
        {
            TryDown();
        }
        else if (diff.y < -0.7f)
        {
            TryJump();
        }



    }


    void TryDown()
    {
        if (canDoDown)
        {
            StartCoroutine(DoDown());
        }
    }


    IEnumerator DoDown()
    {
        canDoDown = false;
        playerCollider.enabled = false;
        yield return new WaitForSeconds(0.03f);
        playerCollider.enabled = true;
        canDoDown = true;
    }
   

  

    public void CheckHit() // Execute In attack Animation
    {
        //Debug.Log("I'm hitting!!");
        float CheckDir = 1f;
        

        if (spriteRenderer.flipX) CheckDir = -1f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0) * CheckDir, 2.5f, enemyLayerMask);
        {
            
            if (hit.collider == null) return;
            //Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.TryGetComponent(out PlayerBattle player))
            {
                player.ChangeHealth(-5);
            }
                
        }

    }


    void OnDash() // when C keyboard input do dash
    {
        if (!canDash) return;

        if (!ghostDash.makeGhost)
        {
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, 0); // gravity done;
            ghostDash.makeGhost = true;
            animator.SetBool(isDashing, true);
            animator.SetBool(isAttacking, false);
            StartCoroutine(DoingDash());
        }


    }

    void OnClick() // When Clicked
    {
        OnAttack();
    }

    IEnumerator DoingDash() // Do Coroutine During Dash
    {
        while (ghostDash.makeGhost)
        {
            if (spriteRenderer.flipX)
            {
                transform.position -= new Vector3(0.2f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0.2f, 0, 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        
        yield return null;
    }

    public void DashOff() //When Dash Animation End
    {
        rigid.gravityScale = playerGravityScale;
        ghostDash.makeGhost = false;
        animator.SetBool(isDashing, false);
    }
    

    void OnRoll() // When Shift called Do Rolling
    {
        if (!canRoll) return;

        if (!Rolling && !Jumping)
        {
            animator.SetBool(isRolling, true);
            Rolling = true;
        }
        
    }

    void ComboStart()
    {
        ComboCount = 0;    
    }

    void ComboSum()
    {
        ComboCount++;
        
    }

    void OnAttack() // Z button called
    {
            //Debug.Log("Attack!!");
        animator.SetBool(isAttacking, true);
        
        if (canCombo && canComboAttack) animator.SetTrigger("NextCombo");

    }

    public void ComboEnable()
    {

        canCombo = true;
        //Debug.Log("ComboEnable");
    }

    public void ComboDisAble()
    {
        canCombo = false;        
    }


    public void AttackEnd()
    {
        //Debug.Log("Combo!!");
        animator.SetBool(isAttacking, false);
    }
    

    public void RollEnd() // When Roll Animation End Called
    {
        animator.SetBool(isRolling, false);
        Rolling = false;
    }


    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (ghostDash.makeGhost) return;

        

        if (transform.position.x - _player.position.x > 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x - _player.position.x < 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
        }
        else
        {
            animator.SetBool(isRunning, false);
        }

        float dir = spriteRenderer.flipX ? -1 : 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(dir * boundPlayer.x, boundPlayer.y), new Vector2(dir, 0), 0.02f, groundLayerMask);
        if (hit.collider?.name != null) return;

        if (Rolling)
        {
            transform.position += moveVelocity * 1.2f * maxSpeed * Time.deltaTime;
            return;
        }


        transform.position += moveVelocity * maxSpeed * Time.deltaTime;
    }

    private void TryJump() // Space Button = Jump
    {
        //Debug.Log(rigid.velocity.y);
        if (Rolling) return;

        if (isGrounded) // && !Jumping
        {
            isGrounded = false;
            
            StartCoroutine(DoJump());
            return;
            //Debug.Log("Try Jumping");
        }
    }

    void JumpCheck() // need to check When player Start Falling Down
    {
        if (rigid.velocity.y < 0 && !isGrounded && Jumping)
        {
            animator.SetBool(isJumping, false);
            animator.SetBool(isFalling, true);
            //Falling = true;
        }
    }



    IEnumerator DoJump() // Give Power
    {
        if (ghostDash.makeGhost) // While During Dash
        {
            rigid.gravityScale = playerGravityScale;
        }
        rigid.AddForce(Vector2.up * jumpPower * rigid.mass, ForceMode2D.Impulse);
        animator.SetBool(isJumping, true);
        yield return new WaitForSeconds(0.1f);
        Jumping = true;
    }


    private void OnCollisionStay2D(Collision2D collider) // Jump and wall Climb check
    {
        //Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.CompareTag(Define.FLOOR_TAG))
        {
            //Debug.Log(boundPlayer.x);
            //Debug.Log(boundPlayer.y);


            for (int i = -1; i < 2; i++)
            {

                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(playerCollider.bounds.extents.x * i,0), new Vector2(0, -1), 0.3f, groundLayerMask); // is Grounded Check
                if (hit.collider?.name != null)
                {
                    //Debug.Log(hit.collider.name);
                    if (!isGrounded && Jumping)
                    {
                        //Falling = false;
                        isGrounded = true;
                        Jumping = false;
                        animator.SetBool(isFalling, false);
                        animator.SetBool(isJumping, false);

                    }
                    return;
                }
            }
            animator.SetBool(isWallClimbing, false);
            //rigid.gravityScale = playerGravityScale;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.BULLET_TAG))
        {
            Debug.Log("BossGotHit!!");
            StartCoroutine(ColorRed());

            float damage = CharacterManager.Instance.Player.stats.attackDamage;
            Debug.Log(damage);
            if (bossHP >= damage)
            {
                bossHP -= damage;
                Debug.Log("남은 체력 : " + (bossHP));
            }
            else
            {
                Debug.Log("사망");
                CallDie();
            }

        }
    }

    void CallDie()
    {
        Debug.Log("Boss Dead!!");
        isBossDie = true;
        animator.SetBool(isRunning, false);
        
    }

    IEnumerator ColorRed()
    {
        spriteRenderer.DOColor(Color.red, 0.2f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.DOColor(Color.white, 0.2f);
            
    }


}
