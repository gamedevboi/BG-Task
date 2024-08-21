using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int inventorysize = 10;
    [SerializeField]
    private InventoryUI inventoryUI;
    [SerializeField]
    private PlayerController playerController;
    
    private void Start() {
        inventoryUI.InitializeInventoryUI(inventorysize);
    }
    
    public void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                playerController.isInInventory = true;
                Cursor.lockState = CursorLockMode.None;

            }else { 
                inventoryUI.Hide();
                playerController.isInInventory = false;
                Cursor.lockState = CursorLockMode.Locked;
                }
    }
  }
}
