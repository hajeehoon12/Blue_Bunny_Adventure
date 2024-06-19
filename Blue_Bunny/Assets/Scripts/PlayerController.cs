using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Collider2D playerCollider;

    [SerializeField]LayerMask groundLayerMask;
    [SerializeField] private float bulletLifeTime;

    Vector2 boundPlayer;

    public float playerSpeed;
    public float jumpPower;
    public float bulletSpeed;

    private bool canJump = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();   
        playerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        boundPlayer = playerCollider.bounds.extents;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        
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
        Debug.Log("Attack!!");
        float dir = spriteRenderer.flipX ? -1 : 1;
        Debug.DrawRay(transform.position + new Vector3(boundPlayer.x * dir, 0, 0), new Vector2(1, 0) * 3 * dir, Color.red);
        GameObject bullet = PoolManager.Instance.Get(0);
        AudioManager.instance.PlayPitchSFX("Shot", 0.03f);// Change Pitch
        bullet.transform.position = transform.position + new Vector3(boundPlayer.x * dir, 0);
        StartCoroutine(BulletLifeTime(bullet, dir));
        
    }

    IEnumerator BulletLifeTime(GameObject bullet, float dir)
    {
        float time = 0f;
        float moveTime = 0.02f;

        while (time < bulletLifeTime)
        {
            bullet.transform.position = bullet.transform.position + new Vector3(dir * 0.02f * bulletSpeed, 0, 0);
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
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }

        transform.position += moveVelocity * playerSpeed * Time.deltaTime;          
    }

    public void OnJump(InputAction.CallbackContext context) // space
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (canJump)
            {
                rigid.AddForce(Vector2.up * jumpPower * rigid.mass, ForceMode2D.Impulse);
                StartCoroutine(ChangeJumpBool());
            }
        }
    }

    IEnumerator ChangeJumpBool()
    {
        yield return new WaitForSeconds(0.01f);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) // Check if player get on floor
    {
        JumpCheck();
    }

    private void JumpCheck() // Check if can jump
    {
        //Debug.Log(boundPlayer.y);
        if (canJump) return;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, boundPlayer.y, 0), new Vector2(0, -1), 0.1f, groundLayerMask);
        //Debug.Log(hit.collider?.name);

        if (hit.collider?.name != null)
        {
            //Debug.Log("onGround");
            canJump = true;
        }

    }





}
