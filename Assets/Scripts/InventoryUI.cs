using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI{
    public class InventoryUI : MonoBehaviour
{
   [SerializeField]
   private InventoryItemUI itemPrefab;
   [SerializeField]
   private RectTransform contentPanel;
   [SerializeField]
   private InventoryDescriptionUI itemDescription;

   [SerializeField]
   private MouseFollowerUI mouseFollowerUI;

    List<InventoryItemUI> listOfItemsUI = new List<InventoryItemUI>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;
    

    private void Awake() {
        Hide();
        mouseFollowerUI.Toggle(false);
        itemDescription.ResetDescription();
    }


    //init inventory ui
    public void InitializeInventoryUI(int inventorysize){
        for (int i = 0 ; i<inventorysize; i++){
            InventoryItemUI itemUI = 
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                itemUI.transform.SetParent(contentPanel);
                listOfItemsUI.Add(itemUI);
                
                itemUI.OnItemClicked += HandleItemSelection;
                itemUI.OnItemBeginDrag += HandleBeginDrag;
                itemUI.OnItemEndDrag += HandleEndDrag;
                itemUI.OnRightMouseBtnClicked += HandleShowItemActions;
                itemUI.OnItemDroppedOn += HandleSwap;
        }
    }

   
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity){
        if (listOfItemsUI.Count > itemIndex){
            listOfItemsUI[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleShowItemActions(InventoryItemUI itemUI)
    {
        // mouseFollowerUI.Toggle(false);
    }

    private void HandleEndDrag(InventoryItemUI itemUI)
    {
        ResetDraggedItem();
        // Debug.Log("End Drag");
    }

    private void HandleSwap(InventoryItemUI itemUI)
    {
        // Debug.Log("Handle swap");

        int idx = listOfItemsUI.IndexOf(itemUI);
        if (idx == -1){
            
            return;
        }

        OnSwapItems?.Invoke(currentlyDraggedItemIndex,idx);
        HandleItemSelection(itemUI);
    }

    private void ResetDraggedItem()
    {
        mouseFollowerUI.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItemUI itemUI)
    {
        // Debug.Log("being drag");
        int idx = listOfItemsUI.IndexOf(itemUI);
        if (idx == -1){
            return;
        }
        currentlyDraggedItemIndex = idx;
        HandleItemSelection(itemUI);
        OnStartDragging?.Invoke(idx);

    }
    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollowerUI.Toggle(true);
        mouseFollowerUI.SetData(sprite,quantity);
    }


    private void HandleItemSelection(InventoryItemUI itemUI)
    {
      int idx = listOfItemsUI.IndexOf(itemUI);
        if (idx == -1){
            return;
        }
        OnDescriptionRequested?.Invoke(idx);
    }

    public void Show(){
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
       foreach (InventoryItemUI item in listOfItemsUI)
       {
        item.Deselect();
       }
    }

    public void Hide(){
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    internal void UpdateDescription(int itemIdx, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfItemsUI[itemIdx].Select();
    }

        internal void ResetAllItems()
        {
            foreach (var item in listOfItemsUI)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
