using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] 
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector2 lastMoveDirection; //用于保存上一次移动方向，防止武器初始方向为（0，0）

    Rigidbody2D rb;
    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(0f,-1);
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate() 
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if(moveX !=  0)
        {
            lastMoveDirection = new Vector2(moveDirection.x, 0f);
        }
        if(moveY != 0)
        {
            lastMoveDirection = new Vector2(0f, moveDirection.y);
        }
        if(moveX != 0 && moveY != 0)
        {
            lastMoveDirection = new Vector2(moveDirection.x, moveDirection.y);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * player.currentMoveSpeed, moveDirection.y * player.currentMoveSpeed);
    }
}
