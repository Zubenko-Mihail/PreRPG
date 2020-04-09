using Items;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public enum ItemType
    {
        Armor,
        Weapon,
        Useable
    }
    public enum ItemRarity
    {
        Common,
        Rare,
        Legendary,
        Mythical
    }
    public enum Useable
    {
        Scroll,
        Potion
    }
    public enum WeaponType
    {
        Sword,
        Bow,
        Mace,
        Scepter
    }
    public enum ArmorType
    {
        Boots,
        Bracers,
        Chestplate,
        Helmet,
        Leggings
    }
}
[Serializable]
public class Item
{
    public ItemRarity itemRarity;
    public string name;
    public GameObject prefab;
    public string pathToPrefabInResources;
    public ItemType itemType;
    public GUI ItemStats;
    public int price;
    public float weight;
}

#region Weapon
public class Weapon : Item
{
    public float minDamage, maxDamage, attackSpeed, range;
    public WeaponType weaponType;
    public Weapon(string name, float minDamage, float maxDamage, float attackSpeed, float weight, float range, ItemRarity itemRarity, WeaponType weaponType, GameObject prefab)
    {
        base.name = name;
        this.prefab = prefab;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.weight = weight;
        this.range = range;
        this.attackSpeed = attackSpeed;
        base.itemRarity = itemRarity;
        this.weaponType = weaponType;
        itemType = ItemType.Weapon;
    }
    public float GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
#endregion
#region Armor
public class Armor : Item
{
    public int physRes, magRes;
    public ArmorType armorType;
    public Armor()
    {
        itemType = ItemType.Armor;
    }
}
public class Boots : Armor
{
    public Boots(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, GameObject prefab)
    {
        base.name = name;
        base.physRes = physRes;
        base.magRes = magRes;
        base.weight = weight;
        base.prefab = prefab;
        base.itemRarity = itemRarity;
        armorType = ArmorType.Boots;
    }
}

public class Bracers : Armor
{
    public Bracers(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, GameObject prefab)
    {
        base.name = name;
        base.physRes = physRes;
        base.magRes = magRes;
        base.weight = weight;
        base.prefab = prefab;
        base.itemRarity = itemRarity;
        armorType = ArmorType.Bracers;
    }
}

public class Chestplate : Armor
{
    public Chestplate(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, GameObject prefab)
    {
        base.name = name;
        base.physRes = physRes;
        base.magRes = magRes;
        base.weight = weight;
        base.prefab = prefab;
        base.itemRarity = itemRarity;
        armorType = ArmorType.Chestplate;
    }
}

public class Helmet : Armor
{
    public Helmet(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, GameObject prefab)
    {
        base.name = name;
        base.physRes = physRes;
        base.magRes = magRes;
        base.weight = weight;
        base.prefab = prefab;
        base.itemRarity = itemRarity;
        armorType = ArmorType.Helmet;
    }
}

public class Leggings : Armor
{
    public Leggings(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, GameObject prefab)
    {
        base.name = name;
        base.physRes = physRes;
        base.magRes = magRes;
        base.weight = weight;
        base.prefab = prefab;
        base.itemRarity = itemRarity;
        armorType = ArmorType.Leggings;
    }
}
#endregion

public abstract class Useable : Item
{
    public int countInStack;
    public Useable(string name, GameObject prefab, ItemType itemType)
    {
        base.name = name;
        base.itemType = itemType;
        base.prefab = prefab;
    }

}