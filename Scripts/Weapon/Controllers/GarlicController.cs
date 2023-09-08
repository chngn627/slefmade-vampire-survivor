using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    //大蒜的攻击方式比较特别，因为大蒜是一直存在不会消失，所以coolDownDuration被当作攻击频率而不是生成频率
    protected override void Start()
    {
        base.Start();
        Attack();
    }

    protected override void Update()
    {
        //父类WeaponController里的Update函数是根据coolDownDuration来调用Attack函数生成新的武器
        //因为大蒜一直存在所以不需要销毁，也不需要重复生成
        //base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position;
        spawnedGarlic.transform.parent = transform; //生成在player下
    }
}
