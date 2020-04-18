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
    public EntityType entityType;
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
    public float damage;
    public float attackRange, attackSpeed;
    public float lookRange;
    [HideInInspector]public EnemyType enemyType;
    public Enemy()
    {
        entityType = EntityType.Enemy;
    }

    public abstract void OnAfterDeserialize();
    public void OnBeforeSerialize() { }
}
public class Dummy : Enemy
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Dummy;
    }
}
[CreateAssetMenu(menuName = "Enemy/Turret", order = 2)]

public class Turret : Enemy 
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Turret;
    }
}

[CreateAssetMenu(menuName = "Enemy/Humanoid", order = 1)]
public class Humanoid : Enemy
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Humanoid;
    }
}



