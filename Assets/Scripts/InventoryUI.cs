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
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }

    


}
