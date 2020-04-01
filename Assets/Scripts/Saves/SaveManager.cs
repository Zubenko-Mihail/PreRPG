using System;
using System.IO;
using UnityEngine;
using Items;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    static string path = "";
    string saveString;
    static SaveData sd = new SaveData();
    [NonSerialized]
    static GameObject player;
    static PlayerStats playerStats;
    static Inventory inventory;
    static BinaryFormatter formatter;
    void Awake()
    {
        sd = new SaveData();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        inventory = player.GetComponent<Inventory>();
        path = Application.dataPath + "/sv.PreRPG";
        formatter = new BinaryFormatter();
        
    }
    public static void SaveGame()
    {
        print("Saving...");
        FileStream stream = new FileStream(path, FileMode.Create);
        SetSave();
        formatter.Serialize(stream, sd);
        print("Saved");
        stream.Close();
    }
    public static void LoadGame()
    {
        if (File.Exists(path))
        {
            print("Loading...");
            FileStream stream = new FileStream(path, FileMode.Open);
            sd = formatter.Deserialize(stream) as SaveData;
            SetValues();
            print("Loaded");
            stream.Close();
        }
    }
    private static void SetSave()
    {
        sd.HP = playerStats.HP;
        SaveInventory();
    }//Установить значения в сохранение
    private static void SaveInventory()
    {
        int i = 0;
        sd.inventoryArray = new string[inventory.InventoryArray.Count];
        sd.inventoryArrayTypes = new ItemType[inventory.InventoryArray.Count];
        foreach (Slot s in inventory.InventoryArray)
        {
            sd.inventoryArray[i] = JsonUtility.ToJson(inventory.InventoryArray[i].Item);
            if(inventory.InventoryArray[i].Item!=null)
                sd.inventoryArrayTypes[i] = inventory.InventoryArray[i].Item.itemType;
            i++;
        }
        sd.Weapon_1 = JsonUtility.ToJson(inventory.Weapon_1.Item);
        sd.Weapon_2 = JsonUtility.ToJson(inventory.Weapon_2.Item);
        sd.Helmet = JsonUtility.ToJson(inventory.Helmet.Item);
        sd.Chestplate = JsonUtility.ToJson(inventory.Chestplate.Item);
        sd.Bracers = JsonUtility.ToJson(inventory.Bracers.Item);
        sd.Leggings = JsonUtility.ToJson(inventory.Leggings.Item);
        sd.Boots = JsonUtility.ToJson(inventory.Boots.Item);
    }//Сохранить инвентарь
    private static void SetValues()
    {
        playerStats.HP = sd.HP;
        SetInventory();
        playerStats.UpdateUI();
        playerStats.UpdateEquipment();
        if(inventory.gameObject.GetComponent<InventoryInteractions>().InventoryPanel.activeSelf==true)
            inventory.Show();
    } //Установить значения из сохранения
    private static void SetInventory()
    {
        int i=0;
        foreach(string json in sd.inventoryArray)
        {
            if(sd.inventoryArray[i]!=null)
                switch (sd.inventoryArrayTypes[i])
                {
                    case ItemType.Weapon:
                        inventory.InventoryArray[i].Item = JsonUtility.FromJson<Weapon>(sd.inventoryArray[i]);
                        break;
                    case ItemType.Armor:
                        inventory.InventoryArray[i].Item = JsonUtility.FromJson<Armor>(sd.inventoryArray[i]);
                        break;
                }
            i++;
        }
        inventory.Weapon_1.Item = JsonUtility.FromJson<Weapon>(sd.Weapon_1);
        inventory.Weapon_2.Item = JsonUtility.FromJson<Weapon>(sd.Weapon_2);
        inventory.Helmet.Item = JsonUtility.FromJson<Helmet>(sd.Helmet);
        inventory.Chestplate.Item = JsonUtility.FromJson<Chestplate>(sd.Chestplate);
        inventory.Bracers.Item = JsonUtility.FromJson<Bracers>(sd.Bracers);
        inventory.Leggings.Item = JsonUtility.FromJson<Leggings>(sd.Leggings);
        inventory.Boots.Item = JsonUtility.FromJson<Boots>(sd.Boots);
        UpdateItemPrefabs();
    }//Установить значения инвентаря
    private static void UpdateItemPrefabs()
    {
        foreach(Slot slot in inventory.InventoryArray)
        {
            if (slot.Item != null)
            {
                Item item = slot.Item;
                SetPrefabToItem(item);
            }
        }
        SetPrefabToItem(inventory.Weapon_1.Item);
        SetPrefabToItem(inventory.Weapon_2.Item);
        SetPrefabToItem(inventory.Helmet.Item);
        SetPrefabToItem(inventory.Chestplate.Item);
        SetPrefabToItem(inventory.Bracers.Item);
        SetPrefabToItem(inventory.Leggings.Item);
        SetPrefabToItem(inventory.Boots.Item);
    }
    private static void SetPrefabToItem(Item item)
    {
        if(item!=null)
            item.prefab = Resources.Load<GameObject>(item.pathToPrefabInResources);
    }
}

[Serializable]
public class SaveData //Класс с сохраненными значениями
{
    public string[] inventoryArray;
    public ItemType[] inventoryArrayTypes;
    public string Weapon_1;
    public string Weapon_2;
    public string Helmet;
    public string Chestplate;
    public string Bracers;
    public string Leggings;
    public string Boots;
    public int HP;
}
