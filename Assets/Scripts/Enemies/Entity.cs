using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Entities
{
    public enum EntityType
    {
        NPC,
        Enemy
    }
    public enum EnemyType
    {
        Dummy,
        Turret,
        Humanoid
    }
}
public abstract class Entity : ScriptableObject
{
    public new string name;
    [HideInInspector]public EntityType entityType;
    public float HP = 100;
    public float maxHP = 100;
    public float speed = 7;
    public GameObject prefab;
    public void GetDamage(Damage Damage)
    {

        float currDamage = Damage.amount;
        HP -= currDamage;

    }

}

public abstract class Enemy : Entity, ISerializationCallbackReceiver
{
    public float damage=5;
    public float attackRange=3, attackSpeed=0.5f;
    public float lookRange=12;
    [HideInInspector]public EnemyType enemyType;
    public Enemy()
    {
        entityType = EntityType.Enemy;
    }

    public abstract void OnAfterDeserialize();
    public void OnBeforeSerialize() { }
}



