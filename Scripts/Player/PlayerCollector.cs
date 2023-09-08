using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 角色的收集器，控制角色收集道具的范围、大小等
public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollider;
    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollider.radius = player.currentMagnet;
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        //检查其他Object是否有 ICOllectible interface
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // 拉近效果
            //获取道具的rigidbody
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //从道具指向玩家的vector2
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //对道具施加forceDirection方向上的力
            rb.AddForce(forceDirection * pullSpeed);

            collectible.Collect();
        }
    }
}
