using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class Drop : MonoBehaviour
{
    public InventoryInteractions Player;
    public Inventory Inventory;

    public Slot DraggingSlot;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryInteractions>();
    }

    public void OnClickDrop()
    {
        if (Player.DraggingSlot != null && Player.DraggingInventory.InventoryType == InventoryType.MainInventory)
        {
            ItemGenerator.SpawnItem(Player.DraggingSlot.Item, Player.transform.position);
            Player.DraggingInventory.Drop();
            Player.DraggingInventory.Show();
            Player.StopDrag();
            Player.playerStats.UpdateEquipment();
        }
    }
}
