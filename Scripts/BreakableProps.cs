using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 挂载到环境中的可破坏物品
public class BreakableProps : MonoBehaviour
{
    public float health;

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
