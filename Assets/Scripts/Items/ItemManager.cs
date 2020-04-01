using Items;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager
{
    static int itemLvlsCount = 5;
    public static readonly Dictionary<ItemRarity, List<List<Item>>> Weapons = new Dictionary<ItemRarity, List<List<Item>>>();
    public static readonly Dictionary<ItemRarity, List<List<Item>>> Armor = new Dictionary<ItemRarity, List<List<Item>>>();
    public static void InitializeItems()
    {
        for (int i = 0; i < 4; i++)
        {
            Weapons[(ItemRarity)i] = new List<List<Item>>();
            for (int j = 0; j <= itemLvlsCount; j++)
            {
                Weapons[(ItemRarity)i].Add(new List<Item>(1) { null });
            }
        }
        for (int i = 0; i < 4; i++)
        {
            Armor[(ItemRarity)i] = new List<List<Item>>();
            for (int j = 0; j <= itemLvlsCount; j++)
            {
                Armor[(ItemRarity)i].Add(new List<Item>(1) { null });
            }
        }

        #region InitializeWeapons
        //string name, float minDamage, float maxDamage, float attackSpeed, float weight, float range, bool isMagical, int itemLvl, ItemRarity itemRarity, string prefabName, WeaponType weaponType
        InitializeWeapon("Just Test Sword", 10, 15, 0.5f, 1, 4, true, 1, ItemRarity.Rare, "Sword", WeaponType.Sword);
        InitializeWeapon("Just Bad Sword", 2, 10, 0.2f, 15, 2, false, 1, ItemRarity.Common, "Otvertka", WeaponType.Sword);
        InitializeWeapon("Лук Воина Летающего Ветра", 5, 8, 1, 1, 2, false, 1, ItemRarity.Common, "Kusachki", WeaponType.Bow);
        InitializeWeapon("Арбалет Воина Летающего Ветра", 10, 15, 0.7f, 1, 2, false, 1, ItemRarity.Common, "Arbalet", WeaponType.Bow);
        InitializeWeapon("Колесо Дорожной Инспекции", 10, 13, 0.5f, 1, 3, true, 1, ItemRarity.Legendary, "Wheel", WeaponType.Sword);
        InitializeWeapon("Двухстволка, почему-то стреляющая лазером", 30, 40, 0.3f, 1, 7, true, 1, ItemRarity.Mythical, "Dvuxstvolka", WeaponType.Scepter);
        InitializeWeapon("Моргенштерн (это булава, а не рэпер)", 25, 30, 0.5f, 3, 3, false, 2, ItemRarity.Rare, "PC", WeaponType.Mace);
        InitializeWeapon("Стиралка (Стирает с лица земли своих врагов)", 0, 100000, 0.2f, 3, 20, true, 2, ItemRarity.Mythical, "Stiralka", WeaponType.Mace);
        InitializeWeapon("Stranniy Blade", 15, 30, 0.4f, 1, 3, true, 2, ItemRarity.Legendary, "Blade", WeaponType.Sword);
        #endregion
        #region InitializeArmor
        //string name, int physRes, int magRes, float weight, int itemLvl, ItemRarity itemRarity, string prefabName, ArmorType armorType
        InitializeArmor("Грудак Космического Рейнджера", 10, 0, 10, 1, ItemRarity.Legendary, "Shcitoc", ArmorType.Chestplate);
        InitializeArmor("Антигравы", 10, 0, 5, 1, ItemRarity.Legendary, "Item", ArmorType.Boots);
        InitializeArmor("Шелм", 10, 0, 5, 1, ItemRarity.Legendary, "Item", ArmorType.Helmet);
        InitializeArmor("Штаны Адидас", 9999, 9999, 10, 1, ItemRarity.Legendary, "microvolnovka", ArmorType.Leggings);
        InitializeArmor("Наручи (Единственное нормальное название)", 5, 5, 3, 1, ItemRarity.Legendary, "Book", ArmorType.Bracers);
        #endregion
    }
    private static void InitializeWeapon(string name, float minDamage, float maxDamage, float attackSpeed, float weight, float range, bool isMagical, int itemLvl, ItemRarity itemRarity, string prefabName, WeaponType weaponType)
    {
        Item item = ItemGenerator.InitializeWeapon(name, minDamage, maxDamage, attackSpeed, weight, range, isMagical, itemRarity, prefabName, weaponType);
        Weapons[itemRarity][itemLvl].Add(item);
    }
    private static void InitializeArmor(string name, int physRes, int magRes, float weight, int itemLvl, ItemRarity itemRarity, string prefabName, ArmorType armorType)
    {
        Item item = ItemGenerator.InitializeArmor(name, physRes, magRes, weight, itemRarity, prefabName, armorType);
        Armor[itemRarity][itemLvl].Add(item);
    }
}
