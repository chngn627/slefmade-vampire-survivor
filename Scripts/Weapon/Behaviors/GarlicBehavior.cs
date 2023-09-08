using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehavior : MeleeBahavior
{
    //大蒜的攻击方式比较特殊
    private bool canDealDamage;

    protected override void Start()
    {
        //因为大蒜一直存在所以不需要父类里面的销毁操作
        //base.Start();
        canDealDamage = true;
    }

    protected override void Update() 
    {
        if(canDealDamage == false)
        {
            if(currentCoolDownDuration <= 0)
            {
                canDealDamage = true;
            }
            else
            {
                currentCoolDownDuration -= Time.deltaTime;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {

    }

    protected override void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && canDealDamage)
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            canDealDamage = false;
        }
        else if(col.CompareTag("Prop") && canDealDamage)
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
            }
        }
    }
}
