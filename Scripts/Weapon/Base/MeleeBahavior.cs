using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBahavior : MonoBehaviour
{
    //近战武器的行为脚本
    public WeaponScriptableObject weaponData;
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

    protected virtual void Update() 
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D col) 
    {
        
    }
}
