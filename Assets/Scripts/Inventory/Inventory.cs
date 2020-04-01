using Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Slot> InventoryArray = new List<Slot>();


    [Space]
    [Range(1, 100)]
    public int Capacity = 1;
    [SerializeField]
    public InventoryType InventoryType;

    #region Equipment Slots
    public Slot Weapon_1 = new Slot(null);
    public Slot Weapon_2 = new Slot(null);
    public Slot Helmet = new Slot(null);
    public Slot Chestplate = new Slot(null);
    public Slot Bracers = new Slot(null);
    public Slot Leggings = new Slot(null);
    public Slot Boots = new Slot(null);

    public GameObject Slot_Weapon_1 = null;
    public GameObject Slot_Weapon_2 = null;
    public GameObject Slot_Helmet = null;
    public GameObject Slot_Chestplate = null;
    public GameObject Slot_Bracers = null;
    public GameObject Slot_Leggings = null;
    public GameObject Slot_Boots = null;
    #endregion

    [HideInInspector]
    public GameObject InventoryUI;
    [HideInInspector]
    public GameObject SlotUI;
    [HideInInspector]
    public InventoryInteractions Player = null;

    public int chosenSlot = 0;

    public float InvWeight = 0;
    PlayerStats playerStats;

    void Awake()
    {
        if (Capacity > 0)
        {
            for (int i = 0; i < Capacity; i++) InventoryArray.Add(new Slot(null));
        }
        else Debug.LogError("Kavo, Capacity Error!");
        InitialisationUI();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryInteractions>();
        SlotUI = Resources.Load<GameObject>("Slot");
        playerStats = GetComponent<PlayerStats>();


        if (InventoryType == InventoryType.MainInventory)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (InventoryArray[i].Item != null)
                {
                    InvWeight += InventoryArray[i].Item.weight;
                }
            }
        }
    }

    void InitialisationUI()
    {
        GameObject UI = GameObject.Find("UI");
        switch (InventoryType)
        {
            case InventoryType.MainInventory:
                {
                    InventoryUI = UI.transform.Find("InvUI/PlayerInventory/Field").gameObject;

                    Slot_Weapon_1 = UI.transform.Find("InvUI/PlayerInventory/Equipment/Weapon_1").gameObject;
                    Slot_Weapon_1.GetComponent<SlotUI>().SlotID = -1;
                    Slot_Weapon_1.GetComponent<SlotUI>().owner = this;

                    Slot_Weapon_2 = UI.transform.Find("InvUI/PlayerInventory/Equipment/Weapon_2").gameObject;
                    Slot_Weapon_2.GetComponent<SlotUI>().SlotID = -2;
                    Slot_Weapon_2.GetComponent<SlotUI>().owner = this;

                    Slot_Helmet = UI.transform.Find("InvUI/PlayerInventory/Equipment/Helmet").gameObject;
                    Slot_Helmet.GetComponent<SlotUI>().SlotID = -3;
                    Slot_Helmet.GetComponent<SlotUI>().owner = this;

                    Slot_Chestplate = UI.transform.Find("InvUI/PlayerInventory/Equipment/Chestplate").gameObject;
                    Slot_Chestplate.GetComponent<SlotUI>().SlotID = -4;
                    Slot_Chestplate.GetComponent<SlotUI>().owner = this;

                    Slot_Bracers = UI.transform.Find("InvUI/PlayerInventory/Equipment/Bracers").gameObject;
                    Slot_Bracers.GetComponent<SlotUI>().SlotID = -5;
                    Slot_Bracers.GetComponent<SlotUI>().owner = this;

                    Slot_Leggings = UI.transform.Find("InvUI/PlayerInventory/Equipment/Leggings").gameObject;
                    Slot_Leggings.GetComponent<SlotUI>().SlotID = -6;
                    Slot_Leggings.GetComponent<SlotUI>().owner = this;

                    Slot_Boots = UI.transform.Find("InvUI/PlayerInventory/Equipment/Boots").gameObject;
                    Slot_Boots.GetComponent<SlotUI>().SlotID = -7;
                    Slot_Boots.GetComponent<SlotUI>().owner = this;
                    break;
                }
        }
    }

    public void Show()
    {
        Hide();

        #region Equipment Show
        if (Weapon_1.Item != null)
        {
            Slot_Weapon_1.GetComponentInChildren<Text>().text = Weapon_1.Item.name;
            Slot_Weapon_1.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Weapon_1.GetComponentInChildren<Text>().text = null;
            Slot_Weapon_1.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Weapon_2.Item != null)
        {
            Slot_Weapon_2.GetComponentInChildren<Text>().text = Weapon_2.Item.name;
            Slot_Weapon_2.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Weapon_2.GetComponentInChildren<Text>().text = null;
            Slot_Weapon_2.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Helmet.Item != null)
        {
            Slot_Helmet.GetComponentInChildren<Text>().text = Helmet.Item.name;
            Slot_Helmet.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Helmet.GetComponentInChildren<Text>().text = null;
            Slot_Helmet.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Chestplate.Item != null)
        {
            Slot_Chestplate.GetComponentInChildren<Text>().text = Chestplate.Item.name;
            Slot_Chestplate.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Chestplate.GetComponentInChildren<Text>().text = null;
            Slot_Chestplate.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Bracers.Item != null)
        {
            Slot_Bracers.GetComponentInChildren<Text>().text = Bracers.Item.name;
            Slot_Bracers.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Bracers.GetComponentInChildren<Text>().text = null;
            Slot_Bracers.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Leggings.Item != null)
        {
            Slot_Leggings.GetComponentInChildren<Text>().text = Leggings.Item.name;
            Slot_Leggings.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Leggings.GetComponentInChildren<Text>().text = null;
            Slot_Leggings.GetComponent<Image>().color = new Color(214, 214, 214);
        }
        if (Boots.Item != null)
        {
            Slot_Boots.GetComponentInChildren<Text>().text = Boots.Item.name;
            Slot_Boots.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Slot_Boots.GetComponentInChildren<Text>().text = null;
            Slot_Boots.GetComponent<Image>().color = new Color(214, 214, 214);
        }

        #endregion

        int length = InventoryArray.Count;

        for (int i = 0; i < length; i++)
        {
            GameObject UI = Instantiate(SlotUI, InventoryUI.transform) as GameObject;

            if (InventoryArray[i].Item == null)
            {
                //UI.GetComponentInChildren<Text>().text = "Empty";
                UI.GetComponent<Image>().color = Color.white;
            }
            else
            {
                UI.GetComponentInChildren<Text>().text = InventoryArray[i].Item.name;
                switch (InventoryArray[i].Item.itemRarity)
                {
                    case ItemRarity.Common: UI.GetComponent<Image>().color = Color.grey; break;
                    case ItemRarity.Rare: UI.GetComponent<Image>().color = Color.yellow; break;
                    case ItemRarity.Mythical: UI.GetComponent<Image>().color = new Color(0, 0.6f, 0.6f); break;
                    case ItemRarity.Legendary: UI.GetComponent<Image>().color = new Color(1, 0.65f, 0); break;
                }
            }

            UI.GetComponent<SlotUI>().SlotID = i;
            UI.GetComponent<SlotUI>().owner = this;
        }
    }

    public void Hide()
    {
        foreach (Transform Child in InventoryUI.transform) Destroy(Child.gameObject);
    }

    public bool AddItem(Item item, int count)
    {
        bool added = false;

        if (item != null)
        {
            switch (item.itemType)
            {
                case ItemType.Weapon:
                    {
                        if (Weapon_1.Item == null)
                        {
                            Weapon_1.Item = item;
                            Weapon_1.Count = 1;
                            added = true;
                        }
                        else if (Weapon_2.Item == null)
                        {
                            Weapon_2.Item = item;
                            Weapon_2.Count = 1;
                            added = true;
                        }
                        break;
                    }
                case ItemType.Armor:
                    {
                        switch (((Armor)item).armorType)
                        {
                            case ArmorType.Helmet:
                                {
                                    if (Helmet.Item == null)
                                    {
                                        Helmet.Item = item;
                                        Helmet.Count = 1;
                                        added = true;
                                    }
                                    break;
                                }
                            case ArmorType.Chestplate:
                                {
                                    if (Chestplate.Item == null)
                                    {
                                        Chestplate.Item = item;
                                        Chestplate.Count = 1;
                                        added = true;
                                    }
                                    break;
                                }
                            case ArmorType.Bracers:
                                {
                                    if (Bracers.Item == null)
                                    {
                                        Bracers.Item = item;
                                        Bracers.Count = 1;
                                        added = true;
                                    }
                                    break;
                                }
                            case ArmorType.Leggings:
                                {
                                    if (Leggings.Item == null)
                                    {
                                        Leggings.Item = item;
                                        Leggings.Count = 1;
                                        added = true;
                                    }
                                    break;
                                }
                            case ArmorType.Boots:
                                {
                                    if (Boots.Item == null)
                                    {
                                        Boots.Item = item;
                                        Boots.Count = 1;
                                        added = true;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                default: break;
            }

            if (!added)
            {
                int lenght = InventoryArray.Count;
                for (int i = 0; i < lenght; i++)
                {
                    if (InventoryArray[i].Item == null)
                    {
                        InventoryArray[i].Item = item;
                        InventoryArray[i].Count = 1;
                        added = true;
                        break;
                    }
                }
            }
        }
        playerStats.UpdateEquipment();
        return added;
    }

    public void Drop()
    {
        switch (chosenSlot)
        {
            case -1:
                {
                    Weapon_1.Item = null;
                    Weapon_1.Count = 0;
                    break;
                }
            case -2:
                {
                    Weapon_2.Item = null;
                    Weapon_2.Count = 0;
                    break;
                }
            case -3:
                {
                    Helmet.Item = null;
                    Helmet.Count = 0;
                    break;
                }
            case -4:
                {
                    Chestplate.Item = null;
                    Chestplate.Count = 0;
                    break;
                }
            case -5:
                {
                    Bracers.Item = null;
                    Bracers.Count = 0;
                    break;
                }
            case -6:
                {
                    Leggings.Item = null;
                    Leggings.Count = 0;
                    break;
                }
            case -7:
                {
                    Boots.Item = null;
                    Boots.Count = 0;
                    break;
                }
            default:
                {
                    InventoryArray[chosenSlot].Item = null;
                    InventoryArray[chosenSlot].Count = 0;
                    break;
                }
        }
    }

    //public bool AddItem(string name, int count) { }

    public void DisplayInv()
    {
        for (int i = 0; i < Capacity; i++)
        {
            Debug.Log(i);
            if (InventoryArray[i].Item != null) Debug.Log(InventoryArray[i].Item.name);
            else Debug.Log("Empty");
        }
        Debug.Log("Weapon_1");
        if (Weapon_1.Item != null) Debug.Log(Weapon_1.Item.name);
        else Debug.Log("Empty");
        Debug.Log("Weapon_2");
        if (Weapon_2.Item != null) Debug.Log(Weapon_2.Item.name);
        else Debug.Log("Empty");
        Debug.Log("Helmet");
        if (Helmet.Item != null) Debug.Log(Helmet.Item.name);
        else Debug.Log("Empty");
    }

    public void CloseUI()
    {
        Slot_Weapon_1.GetComponent<SlotUI>().OnExit();
        Slot_Weapon_2.GetComponent<SlotUI>().OnExit();
        Slot_Helmet.GetComponent<SlotUI>().OnExit();
        Slot_Chestplate.GetComponent<SlotUI>().OnExit();
        Slot_Bracers.GetComponent<SlotUI>().OnExit();
        Slot_Leggings.GetComponent<SlotUI>().OnExit();
        Slot_Boots.GetComponent<SlotUI>().OnExit();
    }

}

public enum InventoryType
{
    MainInventory,
    Chest,
    Shop,
    Craft
}
