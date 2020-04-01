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
public abstract class Entity
{
    public string name;
    public EntityType entityType;
    public float HP = 100;
    public float maxHP = 100;
    public float speed = 7;
    public void GetDamage(Damage Damage)
    {

        float currDamage = Damage.amount;
        HP -= currDamage;

    }
}

public abstract class Enemy : Entity
{
    public float damage;
    public float attackRange, attackSpeed;
    public float lookRange;
    public EnemyType enemyType;
    public Enemy()
    {
        entityType = EntityType.Enemy;
    }
}
public class Dummy : Enemy
{
    public Dummy(string name, float damage, int HP)
    {
        enemyType = EnemyType.Dummy;
        base.name = name;
        base.damage = damage;
        base.HP = HP;
        maxHP = HP;
        attackRange = 2;
    }
}

public class Turret : Enemy
{
    public Turret(string name, float damage, int HP)
    {
        enemyType = EnemyType.Turret;
        base.name = name;
        base.damage = damage;
        base.HP = HP;
        maxHP = HP;
        attackRange = 5;
        attackSpeed = 0.3f;
        lookRange = 10;
    }
}

public class Humanoid : Enemy
{
    public Humanoid(string name, float damage, int HP)
    {
        enemyType = EnemyType.Humanoid;
        base.name = name;
        base.damage = damage;
        base.HP = HP;
        maxHP = HP;
        attackRange = 1;
        attackSpeed = 0.6f;
        lookRange = 10;
    }
}



