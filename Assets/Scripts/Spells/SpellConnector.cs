using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spells;

public class SpellConnector : MonoBehaviour
{
    Spell spell;
    Vector3 startPos;
    GameObject test;
    public void SetSpellParams(Spell currSpell, Vector3 target)
    {
        spell = currSpell;
        ((PointTargetSpell)spell).target = target;
    }
    public void SetSpellParams(Spell currSpell, GameObject target)
    {
        spell = currSpell;
        ((EnemyTargetSpell)spell).target = target;
    }
    public void SetSpellParams(Spell currSpell, Vector3 target, float radius)
    {
        spell = currSpell;
        ((AreaOfEffectSpell)spell).target = target;
        ((AreaOfEffectSpell)spell).radius = radius;
    }

    void CheckSpellEffect()
    {
        switch (spell.spellType)
        {
            case SpellType.EnemyTargetSpell:
                EnemyTargetSpellEffect();
                break;
            case SpellType.PointTargetSpell:
                PointTargetSpellEffect();
                break;
            case SpellType.AreaOfEffectSpell:
                AreaOfEffectSpell();
                break;
        }
    }
    
    void PointTargetSpellEffect()
    {
        transform.position+=(((PointTargetSpell)spell).target-startPos).normalized / 3;
    }
    void AreaOfEffectSpell()
    {
        transform.position = Vector3.MoveTowards(transform.position, ((AreaOfEffectSpell)spell).target, 0.5f);
        if (transform.position == ((AreaOfEffectSpell)spell).target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ((AreaOfEffectSpell)spell).radius);
            foreach (Collider i in hitColliders)
            {
                if (i.gameObject.tag == "Enemy")
                {
                    i.GetComponent<EnemyConnector>().GetDamage(new Damage((int)spell.damage));
                }
            }
            test = Instantiate(Resources.Load<GameObject>("Prefabs/Sphere"), transform.position, transform.rotation);
            test.transform.localScale = new Vector3(((AreaOfEffectSpell)spell).radius, ((AreaOfEffectSpell)spell).radius, ((AreaOfEffectSpell)spell).radius);
            Destroy(gameObject);
            Destroy(test, 3);
        }
    }
    void EnemyTargetSpellEffect()
    {
        if(((EnemyTargetSpell)spell).target!=null)
        transform.position += (((EnemyTargetSpell)spell).target.transform.position-transform.position).normalized / 10;
        else Destroy(gameObject);
    }
    private void Awake()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        CheckSpellEffect();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(spell.spellType==SpellType.EnemyTargetSpell)
        {
            if (other.gameObject == ((EnemyTargetSpell)spell).target)
            {
                other.GetComponent<EnemyConnector>().GetDamage(new Damage((int)spell.damage));
                Destroy(gameObject);
            }
        }
        if(spell.spellType == SpellType.AreaOfEffectSpell)
        {
            if (other.gameObject.tag=="Enemy")
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, ((AreaOfEffectSpell)spell).radius);
                foreach (Collider i in hitColliders)
                {
                    if (i.gameObject.tag == "Enemy")
                    {
                        i.GetComponent<EnemyConnector>().GetDamage(new Damage((int)spell.damage));
                    }
                }
                test = Instantiate(Resources.Load<GameObject>("Prefabs/Sphere"), transform.position, transform.rotation);
                test.transform.localScale = new Vector3(((AreaOfEffectSpell)spell).radius, ((AreaOfEffectSpell)spell).radius, ((AreaOfEffectSpell)spell).radius);
                Destroy(gameObject);
                Destroy(test, 3);
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyConnector>().GetDamage(new Damage((int)spell.damage));
                Destroy(gameObject);
            }
        }

    }
}
