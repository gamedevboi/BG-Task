using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory{
public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryUI inventoryUI;
    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private PlayerController playerController;


    public List<InventoryItem> initialItems = new List<InventoryItem>();
    
    private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty){
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> invState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in invState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDragging;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }


    private void HandleItemActionRequest(int itemIdx)
    {
    }

    private void HandleDragging(int itemIdx)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIdx);
        if (inventoryItem.IsEmpty){
            return;
        }
        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIdx_1, int itemIdx_2)
    {
        inventoryData.SwapItems(itemIdx_1, itemIdx_2);
    }

    private void HandleDescriptionRequest(int itemIdx)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIdx);
        if (inventoryItem.IsEmpty){
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIdx,  item.ItemImage, item.name, item.Description);
    }



    public void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                playerController.isInInventory = true;
                Cursor.lockState = CursorLockMode.None;

                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }


            }else { 
                inventoryUI.Hide();
                playerController.isInInventory = false;
                Cursor.lockState = CursorLockMode.Locked;
                }
    }
  }
}
}



