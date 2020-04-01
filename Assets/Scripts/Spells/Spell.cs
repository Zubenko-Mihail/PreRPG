using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spells
{
    public enum SpellType
    {
        EnemyTargetSpell,
        PointTargetSpell,
        AreaOfEffectSpell
    }
    public abstract class Spell
    {
        public GameObject prefab;
        public float damage;
        public string name;
        public SpellType spellType;
        public int manaCost;
    }
    public class PointTargetSpell : Spell
    {
        public Vector3 target;
        public PointTargetSpell(string name, float damage, int manaCost)
        {
            base.damage = damage;
            base.name = name;
            base.manaCost = manaCost;
            base.spellType = SpellType.PointTargetSpell;
        }
    }

    public class EnemyTargetSpell : Spell
    {
        public GameObject target;
        public EnemyTargetSpell(string name, float damage, int manaCost)
        {
            base.damage = damage;
            base.name = name;
            base.manaCost = manaCost;
            base.spellType = SpellType.EnemyTargetSpell;
        }
    }

    public class AreaOfEffectSpell : Spell
    {
        public Vector3 target;
        public float radius;
        public AreaOfEffectSpell(string name, float damage, int manaCost, float rad)
        {
            radius = rad;
            base.damage = damage;
            base.name = name;
            base.manaCost = manaCost;
            base.spellType = SpellType.AreaOfEffectSpell;
        }
    }

}