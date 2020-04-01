using UnityEditor;
using UnityEngine;

namespace Items
{
    public class ItemGenerator : MonoBehaviour
    {
        public static Item InitializeWeapon(string name, float minDamage, float maxDamage, float attackSpeed, float weight, float range, bool isMagical, ItemRarity itemRarity, string prefabName, WeaponType weaponType)
        {
            GameObject go;
            Item item;
            go = Resources.Load<GameObject>("Prefabs/Weapons/" + prefabName);
            item = new Weapon(name, minDamage, maxDamage, attackSpeed, weight, range, itemRarity, weaponType, go);
            item.pathToPrefabInResources = "Prefabs/Weapons/" + prefabName;
            return item;
        }
        public static Item InitializeArmor(string name, int physRes, int magRes, float weight, ItemRarity itemRarity, string prefabName, ArmorType armorType)
        {
            GameObject go;
            Item item;
            go = Resources.Load<GameObject>("Prefabs/Armor/" + prefabName);
            switch (armorType)
            {
                case ArmorType.Boots: item = new Boots(name, physRes, magRes, weight, itemRarity, go); break;
                case ArmorType.Bracers: item = new Bracers(name, physRes, magRes, weight, itemRarity, go); break;
                case ArmorType.Chestplate: item = new Chestplate(name, physRes, magRes, weight, itemRarity, go); break;
                case ArmorType.Helmet: item = new Helmet(name, physRes, magRes, weight, itemRarity, go); break;
                case ArmorType.Leggings: item = new Leggings(name, physRes, magRes, weight, itemRarity, go); break;
                default: item = null; break;
            }
            item.pathToPrefabInResources = "Prefabs/Armor/" + prefabName;
            return item;
        }

        public static Item CreateWeapon(Item item)
        {
            Weapon ret;
            Weapon _item = (Weapon)item;
            float minDamageLowering = 0;
            float minDamageUpping = 0;
            float maxDamageLowering = 0;
            float maxDamageUpping = 0;
            //Initializing Upping and Lowering percent
            switch (_item.itemRarity)
            {
                case ItemRarity.Common:
                    minDamageLowering = 0.3f;
                    minDamageUpping = 0.3f;
                    maxDamageLowering = 0.2f;
                    maxDamageUpping = 0.1f;
                    break;
                case ItemRarity.Rare:
                    minDamageLowering = 0.2f;
                    minDamageUpping = 0.3f;
                    maxDamageLowering = 0.1f;
                    maxDamageUpping = 0.15f;
                    break;
                case ItemRarity.Legendary:
                    minDamageLowering = 0.1f;
                    minDamageUpping = 0.35f;
                    maxDamageLowering = 0.15f;
                    maxDamageUpping = 0.25f;
                    break;
                case ItemRarity.Mythical:
                    minDamageLowering = 0.05f;
                    minDamageUpping = 0.4f;
                    maxDamageLowering = 0.1f;
                    maxDamageUpping = 0.3f;
                    break;
            }
            //Randomizing Stats
            ret = new Weapon(_item.name, _item.minDamage + _item.minDamage * Random.Range(-minDamageLowering, minDamageUpping),
                _item.maxDamage + _item.maxDamage * Random.Range(-maxDamageLowering, maxDamageUpping), _item.attackSpeed, _item.weight, _item.range,
                _item.itemRarity, _item.weaponType, _item.prefab);
            ret.pathToPrefabInResources = item.pathToPrefabInResources;
            return ret;
        }
        public static void SpawnItem(Item item, Vector3 pos)
        {
            if (item != null)
            {
                GameObject go = Instantiate(item.prefab, pos + item.prefab.transform.localPosition, item.prefab.transform.rotation);
                go.GetComponent<ItemConnector>().item = item;
            }
        }
    }
}