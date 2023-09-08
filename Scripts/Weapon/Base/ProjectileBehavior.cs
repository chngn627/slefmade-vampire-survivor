 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    //投射武器的行为脚本
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;
    //当前数值
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCoolDownDuration;
    protected int currentPierce;

    void Awake() 
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCoolDownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir) //控制武器初始方向和与东方想
    {
        direction = dir; //初始运动朝向

        //prefab朝向
        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0) //左
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry < 0) //下
        {
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry > 0) //上
        {
            scale.x = scale.x * -1;
        }
        else if(dirx > 0 && diry > 0) //右上
        {
            rotation.z = 0f;
        }
        else if(dirx > 0 && diry <0) //右下
        {
            rotation.z = -90f;
        }
        else if(dirx < 0 && diry > 0) //左上
        {
            rotation.z = 90f;
        }
        else if(dirx < 0 && diry < 0) //左下
        {
            rotation.z = 180f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    void ReducePierce() //飞行武器的穿刺数到0时摧毁武器
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
