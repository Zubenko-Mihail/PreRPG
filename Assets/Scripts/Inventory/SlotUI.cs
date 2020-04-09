using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotUI : MonoBehaviour
{
    public int SlotID = 0;
    public Inventory owner;
    public bool Enter = false;
    public Slot SlotItem = new Slot(null);

    public void Clicked()
    {
        Slot SlotCopy = null;

        switch (SlotID)
        {
            case -1: SlotCopy = owner.Weapon_1; break;
            case -2: SlotCopy = owner.Weapon_2; break;
            case -3: SlotCopy = owner.Helmet; break;
            case -4: SlotCopy = owner.Chestplate; break;
            case -5: SlotCopy = owner.Bracers; break;
            case -6: SlotCopy = owner.Leggings; break;
            case -7: SlotCopy = owner.Boots; break;
            default: SlotCopy = owner.InventoryArray[SlotID]; break;
        }

        Debug.Log(SlotID);
        Debug.Log(owner.chosenSlot);

        if (owner.Player.IsDragging)
        {
            if (/*#1*/
                    owner.chosenSlot >= 0 && SlotID >= 0 ||
                /*#2-3*/
                    (owner.chosenSlot >= 0 && SlotID < 0 || owner.chosenSlot < 0 && SlotID < 0) &&
                  (((SlotID == -1 || SlotID == -2) && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Weapon) ||
                    (SlotID == -3 && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Armor && ((Armor)owner.Player.DraggingSlot.Item).armorType == Items.ArmorType.Helmet) ||
                    (SlotID == -4 && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Armor && ((Armor)owner.Player.DraggingSlot.Item).armorType == Items.ArmorType.Chestplate) ||
                    (SlotID == -5 && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Armor && ((Armor)owner.Player.DraggingSlot.Item).armorType == Items.ArmorType.Bracers) ||
                    (SlotID == -6 && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Armor && ((Armor)owner.Player.DraggingSlot.Item).armorType == Items.ArmorType.Leggings) ||
                    (SlotID == -7 && owner.Player.DraggingSlot.Item.itemType == Items.ItemType.Armor && ((Armor)owner.Player.DraggingSlot.Item).armorType == Items.ArmorType.Boots)) ||
                /*#4*/
                    owner.chosenSlot < 0 && SlotID >= 0 &&
                    (owner.InventoryArray[SlotID].Item == null ||
                   ((owner.chosenSlot == -1 || owner.chosenSlot == -2) && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Weapon) ||
                    (owner.chosenSlot == -3 && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Armor && ((Armor)owner.InventoryArray[SlotID].Item).armorType == Items.ArmorType.Helmet) ||
                    (owner.chosenSlot == -4 && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Armor && ((Armor)owner.InventoryArray[SlotID].Item).armorType == Items.ArmorType.Chestplate) ||
                    (owner.chosenSlot == -5 && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Armor && ((Armor)owner.InventoryArray[SlotID].Item).armorType == Items.ArmorType.Bracers) ||
                    (owner.chosenSlot == -6 && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Armor && ((Armor)owner.InventoryArray[SlotID].Item).armorType == Items.ArmorType.Leggings) ||
                    (owner.chosenSlot == -7 && owner.InventoryArray[SlotID].Item.itemType == Items.ItemType.Armor && ((Armor)owner.InventoryArray[SlotID].Item).armorType == Items.ArmorType.Boots)))
            {
                if (SlotCopy.Item == null)
                {
                    if (owner.InventoryType == InventoryType.Shop && owner.Player.DraggingInventory.InventoryType == InventoryType.MainInventory ||
                        owner.InventoryType == InventoryType.MainInventory && owner.Player.DraggingInventory.InventoryType == InventoryType.Shop)
                    {
                        owner.Player.DraggingInventory.MONEY += owner.Player.DraggingSlot.Item.price;
                        owner.MONEY -= owner.Player.DraggingSlot.Item.price;
                    }

                    SlotCopy.Count = owner.Player.DraggingSlot.Count;
                    SlotCopy.Item = owner.Player.DraggingSlot.Item;
                    owner.Player.DraggingSlot.Item = null;
                    owner.Show();
                }
                else
                {
                    if (owner.InventoryType == InventoryType.Shop && owner.Player.DraggingInventory.InventoryType == InventoryType.MainInventory ||
                        owner.InventoryType == InventoryType.MainInventory && owner.Player.DraggingInventory.InventoryType == InventoryType.Shop)
                    {
                        owner.Player.DraggingInventory.MONEY += owner.Player.DraggingSlot.Item.price;
                        owner.MONEY -= owner.Player.DraggingSlot.Item.price;
                        owner.Player.DraggingInventory.MONEY -= SlotCopy.Item.price;
                        owner.MONEY += SlotCopy.Item.price;
                    }

                    Slot draggingSlot = new Slot(owner.Player.DraggingSlot.Item);

                    draggingSlot.Count = owner.Player.DraggingSlot.Count;
                    draggingSlot.Item = owner.Player.DraggingSlot.Item;

                    owner.Player.DraggingSlot.Count = SlotCopy.Count;
                    owner.Player.DraggingSlot.Item = SlotCopy.Item;

                    SlotCopy.Count = draggingSlot.Count;
                    SlotCopy.Item = draggingSlot.Item;
                }
                owner.Player.StopDrag();
                owner.Player.DraggingInventory.Show();
                owner.Show();
            }
        }
        else
        {
            transform.GetComponentInChildren<Text>().text = null;
            transform.GetComponent<Image>().color = new Color(214, 214, 214);
            owner.Player.SlotClicked(SlotCopy, owner);
            owner.chosenSlot = SlotID;
        }
    }

    public void SlotRefresh()
    {
        switch (SlotID)
        {
            case -1: SlotItem = owner.Weapon_1; break;
            case -2: SlotItem = owner.Weapon_2; break;
            case -3: SlotItem = owner.Helmet; break;
            case -4: SlotItem = owner.Chestplate; break;
            case -5: SlotItem = owner.Bracers; break;
            case -6: SlotItem = owner.Leggings; break;
            case -7: SlotItem = owner.Boots; break;
            default: SlotItem = owner.InventoryArray[SlotID]; break;
        }
    }

    public void OnEnter()
    {
        Enter = true;
        SlotRefresh();
    }

    public void OnExit()
    {
        Enter = false;
    }

    public void OnGUI()
    {
        if (Enter && SlotItem.Item != null)
        {
            string Name = SlotItem.Item.name;
            string Type = "";
            string TypeOfType = "";
            string Rarity = "";
            string Result = "";

            switch (SlotItem.Item.itemRarity)
            {
                case Items.ItemRarity.Common:
                    {
                        Rarity = "Common";
                        GUI.color = Color.white;
                        break;
                    }
                case Items.ItemRarity.Rare:
                    {
                        Rarity = "Rare";
                        GUI.color = Color.blue;
                        break;
                    }
                case Items.ItemRarity.Legendary:
                    {
                        Rarity = "Legendary";
                        GUI.color = Color.yellow;
                        break;
                    }
                case Items.ItemRarity.Mythical:
                    {
                        Rarity = "Mythical";
                        GUI.color = Color.magenta;
                        break;
                    }
            }


            switch (SlotItem.Item.itemType)
            {
                case Items.ItemType.Armor:
                    {
                        Type = "Armor";
                        switch (((Armor)SlotItem.Item).armorType)
                        {
                            case Items.ArmorType.Helmet:
                                {
                                    TypeOfType = "Helmet";
                                    break;
                                }
                            case Items.ArmorType.Chestplate:
                                {
                                    TypeOfType = "Chestplate";
                                    break;
                                }
                            case Items.ArmorType.Bracers:
                                {
                                    TypeOfType = "Bracers";
                                    break;
                                }
                            case Items.ArmorType.Leggings:
                                {
                                    TypeOfType = "Leggins";
                                    break;
                                }
                            case Items.ArmorType.Boots:
                                {
                                    TypeOfType = "Boots";
                                    break;
                                }
                        }

                        Result = Name + 
                        "\nType: " + Type + 
                        "\n" + Type + " type: " + TypeOfType + 
                        "\nRarity: " + Rarity +
                        "\nPhysical Resistance: " + ((Armor)SlotItem.Item).physRes.ToString() + 
                        "\nMagical Resistance: " + ((Armor)SlotItem.Item).magRes.ToString() +
                        "\nCost: " + SlotItem.Item.price;

                        GUI.skin.box.fontSize = 15;
                        GUI.Box(new Rect(Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y + 10, 5 * Result.Length, GUI.skin.box.fontSize + 120), Result);

                        break;
                    }
                case Items.ItemType.Weapon:
                    {
                        Type = "Weapon";
                        switch (((Weapon)SlotItem.Item).weaponType)
                        {
                            case Items.WeaponType.Sword:
                                {
                                    TypeOfType = "Sword";
                                    break;
                                }
                            case Items.WeaponType.Mace:
                                {
                                    TypeOfType = "Mace";
                                    break;
                                }
                            case Items.WeaponType.Bow:
                                {
                                    TypeOfType = "Bow";
                                    break;
                                }
                            case Items.WeaponType.Scepter:
                                {
                                    TypeOfType = "Scepter";
                                    break;
                                }
                        }

                        Result = Name +
                        "\nType: " + Type +
                        "\n" + Type + " type: " + TypeOfType +
                        "\nRarity: " + Rarity +
                        "\nDamage: " + ((Weapon)SlotItem.Item).minDamage.ToString() + "-" + ((Weapon)SlotItem.Item).maxDamage.ToString() +
                        "\nAttack Speed: " + ((Weapon)SlotItem.Item).attackSpeed.ToString() +
                        "\nAttack Range: " + ((Weapon)SlotItem.Item).range.ToString() +
                        "\nCost: " + SlotItem.Item.price;

                        GUI.skin.box.fontSize = 15;
                        GUI.Box(new Rect(Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y + 10, 4 * Result.Length, GUI.skin.box.fontSize + 150), Result);

                        break;
                    }
                case Items.ItemType.Useable:
                    {
                        Type = "Useable";
                        break;
                    }
            }
        }
    }
}
