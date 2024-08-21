using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
   [SerializeField]
   private InventoryItemUI itemPrefab;
   [SerializeField]
   private RectTransform contentPanel;

   List<InventoryItemUI> listOfItemsUI = new List<InventoryItemUI>();

    //init inventory ui
    public void InitializeInventoryUI(int inventorysize){
        for (int i = 0 ; i<inventorysize; i++){
            InventoryItemUI itemUI = 
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                itemUI.transform.SetParent(contentPanel);
                listOfItemsUI.Add(itemUI);
                
                itemUI.OnItemClicked += HandleItemSelection;
                itemUI.OnItemBeingDrag += HandleBeingDrag;
                itemUI.OnItemDroppedOn += HandleSwap;
                itemUI.OnItemEndDrag += HandleEndDrag;
                itemUI.OnRightMouseBtnClicked += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(InventoryItemUI uI)
    {
    }

    private void HandleEndDrag(InventoryItemUI uI)
    {
    }

    private void HandleSwap(InventoryItemUI uI)
    {
    }

    private void HandleBeingDrag(InventoryItemUI uI)
    {
    }

    private void HandleItemSelection(InventoryItemUI uI)
    {
        Debug.Log(uI.name);
    }

    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }




}
