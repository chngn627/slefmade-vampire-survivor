using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    //武器基本参数
    [SerializeField]
    float damage; //伤害
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed; //速度
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration; // 冷却时间
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int pierce; //武器的最大穿刺次数
    public int Pierce { get => pierce; private set => pierce = value; }
}
