using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;


    public float playerSpeed;
    public float jumpPower;

    private bool canJump;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        JumpCheck();
    }


    private void Move()
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

    private void OnJump()
    {
        rigid.AddForce(Vector2.up * jumpPower * rigid.mass, ForceMode2D.Impulse);

    }

    private void JumpCheck()
    { 
        
    }





}
