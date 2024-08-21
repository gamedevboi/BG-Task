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
   [SerializeField]
   private InventoryDescriptionUI itemDescription;

   List<InventoryItemUI> listOfItemsUI = new List<InventoryItemUI>();


    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake() {
        Hide();
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
        itemDescription.SetDescription(image, title,description);
    }

    public void Show(){
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        listOfItemsUI[0].SetData(image,quantity);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }




}
