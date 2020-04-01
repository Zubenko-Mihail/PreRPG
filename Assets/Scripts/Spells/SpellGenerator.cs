using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spells;

public class SpellGenerator : MonoBehaviour
{
    public static GameObject player, spellItself;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public static Spell CreateSpell(string name, float damage, int manaCost, SpellType spelltype, string prefabPath, float radius)
    {
        Spell ret;     
        ret = new AreaOfEffectSpell(name, damage, manaCost, radius);
        ret.prefab = Resources.Load<GameObject>(prefabPath);
        return ret;
    }

   public static Spell CreateSpell(string name, float damage, int manaCost, SpellType spelltype, string prefabPath)
    {
        Spell ret;
        switch (spelltype)
        {
            case SpellType.EnemyTargetSpell:
                ret = new EnemyTargetSpell(name, damage, manaCost);
                break;
            case SpellType.PointTargetSpell:
                ret = new PointTargetSpell(name, damage, manaCost);
                break;
            default: ret = null; break;
        }
        ret.prefab = Resources.Load<GameObject>(prefabPath);
        return ret;
    }

    public static void SpawnSpell(Spell spell, Vector3 position, Quaternion rotation, Vector3 target)
    {

        spellItself = Instantiate(spell.prefab, position, rotation);
        spellItself.GetComponent<SpellConnector>().SetSpellParams(spell, target);
    }
    public static void SpawnSpell(Spell spell, Vector3 position, Quaternion rotation, GameObject target)
    {

        spellItself = Instantiate(spell.prefab, position, rotation);
        spellItself.GetComponent<SpellConnector>().SetSpellParams(spell, target);
    }
    public static void SpawnSpell(Spell spell, Vector3 position, Quaternion rotation, Vector3 target, float radius)
    {

        spellItself = Instantiate(spell.prefab, position, rotation);
        spellItself.GetComponent<SpellConnector>().SetSpellParams(spell, target, radius);
    }
}
