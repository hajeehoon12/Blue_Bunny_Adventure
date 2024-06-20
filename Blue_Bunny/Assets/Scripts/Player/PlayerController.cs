using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Collider2D playerCollider;
    Animator animator;
    GhostDash ghostDash;
    PlayerBattle playerBattle;

    private static readonly int isMoving = Animator.StringToHash("IsMoving");

    [SerializeField]LayerMask groundLayerMask;
    [SerializeField] private float bulletLifeTime;

    Vector2 boundPlayer;


    private float playerGravityScale;

    private bool canJump = true;
    private bool canDash = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();   
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        ghostDash = GetComponent<GhostDash>();
        playerBattle = GetComponent<PlayerBattle>();
    }

    private void Start()
    {
        boundPlayer = playerCollider.bounds.extents;
        playerGravityScale = rigid.gravityScale;

        playerBattle.OnDamage += GetAttacked;
    }

    

    private void FixedUpdate()
    {
        Move();
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BulletAttack();
        }
    }


    void BulletAttack()
    {
        //Debug.Log("Attack!!");
        float dir = spriteRenderer.flipX ? -1 : 1;
        Debug.DrawRay(transform.position + new Vector3(boundPlayer.x * dir, boundPlayer.y, 0), new Vector2(1, 0) * 3 * dir, Color.red);
        GameObject bullet = PoolManager.Instance.Get(0);
        AudioManager.instance.PlayPitchSFX("Shot", 0.03f);// Change Pitch
        bullet.transform.position = transform.position + new Vector3(boundPlayer.x * dir, boundPlayer.y);
        bullet.transform.eulerAngles = new Vector3(0, 0, 90 - dir * 90);
        StartCoroutine(BulletLifeTime(bullet, dir));
        
    }

    IEnumerator BulletLifeTime(GameObject bullet, float dir)
    {
        float time = 0f;
        float moveTime = 0.02f;

        while (time < bulletLifeTime)
        {
            bullet.transform.position = bullet.transform.position + new Vector3(dir * 0.02f * CharacterManager.Instance.Player.stats.bulletSpeed, 0, 0);
            time += moveTime;
            yield return new WaitForSeconds(moveTime);
        }
        bullet.SetActive(false);
        
    }




    private void Move() // basic Move
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if(canJump) animator.SetBool(isMoving, true);
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if(canJump) animator.SetBool(isMoving, true);
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool(isMoving, false);
        }

        transform.position += moveVelocity * CharacterManager.Instance.Player.stats.playerSpeed * Time.deltaTime;          
    }

    public void OnJump(InputAction.CallbackContext context) // space
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (canJump)
            {
                animator.SetBool(isMoving, false);
                rigid.AddForce(Vector2.up * CharacterManager.Instance.Player.stats.jumpPower * rigid.mass, ForceMode2D.Impulse);
                StartCoroutine(ChangeJumpBool());
            }
        }
    }

    IEnumerator ChangeJumpBool()
    {
        yield return new WaitForSeconds(0.01f);
        canJump = false;
        animator.SetBool(isMoving, false);
    }

    

    private void JumpCheck() // Check if can jump
    {
        //Debug.Log(boundPlayer.y);
        if (canJump) return;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position , new Vector2(0, -1), 0.1f, groundLayerMask);
        //Debug.Log(hit.collider?.name);

        if (hit.collider?.name != null)
        {
            //Debug.Log("onGround");
            canJump = true;
        }

    }


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (canDash)
            {
                
                rigid.gravityScale = 0f;
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                ghostDash.makeGhost = true;
                StartCoroutine(DoingDash());
                StartCoroutine(DashOff());
            }
        }
    }

    IEnumerator DoingDash()
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

    IEnumerator DashOff()
    {
        yield return new WaitForSeconds(0.2f);
        rigid.gravityScale = playerGravityScale;
        ghostDash.makeGhost = false;
    }

    void HitCheck(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Define.MONSTER_TAG))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            playerBattle.ChangeHealth(-monster.Data.AttackDamage); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Check if player get on floor
    {
        JumpCheck();

        HitCheck(collision);
        //Debug.Log("Hit");
        
    }

    private void GetAttacked()
    {
        Debug.Log("Do Red");

        float knockBackPower = 5f;
        float Dir = spriteRenderer.flipX ? -1 : 1;

        StartCoroutine(ColorChanged());
        rigid.AddForce( (Vector2.up + Dir * new Vector2(1, 0)) * rigid.mass * knockBackPower , ForceMode2D.Impulse);
        
        
    }

    IEnumerator ColorChanged()
    {
        float durTime = playerBattle.healthChangeDelay / 2;
        spriteRenderer.DOColor(Color.red, durTime);
        yield return new WaitForSeconds(durTime);
        spriteRenderer.DOColor(Color.white, durTime);

    }

}
