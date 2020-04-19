﻿using Items;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/*
 * BABUSHKA TEHNAR
 DED GUMANITARIJ
 YA IZOBREL UNIVERSALNIJ KOMMENTARIJ
 */


public class InventoryInteractions : MonoBehaviour
{
    [HideInInspector]
    public GameObject InventoryPanel;
    [HideInInspector]
    public GameObject ShopPanel;
    [HideInInspector]
    public GameObject UI;

    Inventory PlayerInventory;
    Controls Controls;

    public bool IsDragging = false;
    public Slot DraggingSlot;
    public Inventory DraggingInventory;
    public GameObject DragIcon;
    public PlayerStats playerStats;
    public Drop Drop;

    [Space]
    public KeyCode PlayerInventoryButton = KeyCode.I;
    public KeyCode Esc = KeyCode.Escape;
    public KeyCode QuickDrop = KeyCode.D;

    void Awake()
    {
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Controls = transform.GetComponent<Controls>();

        UI = GameObject.Find("UI");
        DragIcon = UI.transform.Find("InvUI/DragIcon").gameObject;
        DragIcon.SetActive(false);

        InventoryPanel = UI.transform.Find("InvUI/PlayerInventory").gameObject;
        InventoryPanel.SetActive(false);
        ShopPanel = UI.transform.Find("InvUI/ShopInventory").gameObject;
        ShopPanel.SetActive(false);

        playerStats = GetComponent<PlayerStats>();

        Drop = UI.transform.Find("InvUI/PlayerInventory/Drop").gameObject.GetComponent<Drop>();
        StopDrag();

    }
    private void Start()
    {
        UsefulThings.inputManager.Gameplay.OpenInventory.performed += _ => ShowInventory();
        UsefulThings.inputManager.Gameplay.OpenInventory.performed += _ => StopDrag();
        
    }

    void Update()
    {
        if (UsefulThings.kb.escapeKey.wasPressedThisFrame && InventoryPanel.activeSelf)
        {
            ShowInventory();
        }

        if (IsDragging)
        {
            DragIcon.transform.position = UsefulThings.mouse.position.ReadValue();
            if (UsefulThings.kb.escapeKey.wasPressedThisFrame || UsefulThings.mouse.rightButton.wasPressedThisFrame) StopDrag();
            if (UsefulThings.kb.dKey.wasPressedThisFrame) Drop.OnClickDrop();
        }
    }

    void ShowInventory()
    {
        if (!InventoryPanel.activeSelf)
        {
            //open
            print("OPEN");
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            PlayerInventory.Show();
        }
        else
        {
            //close
            PlayerInventory.CloseUI();
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            ShopPanel.SetActive(false);
        }
    }

    public void ShowShop()
    {
        ShopPanel.SetActive(true);
        if (!InventoryPanel.activeSelf)
        {
            ShowInventory();
        }
    }

    public void SlotClicked(Slot slot, Inventory inv)
    {
        if (slot.Item != null)
        {
            DraggingInventory = inv;
            StartDrag(slot);
        }
    }

    public void StartDrag(Slot slot)
    {
        IsDragging = true;
        Controls.IsDragging = true;
        DraggingSlot = slot;
        DragIcon.SetActive(true);
        DragIcon.GetComponentInChildren<Text>().text = slot.Item.name;
    }

    public void StopDrag()
    {
        print("stop");
        IsDragging = false;
        Controls.IsDragging = false;
        DraggingSlot = null;
        DragIcon.SetActive(false);
        playerStats.UpdateEquipment();
    }

}
