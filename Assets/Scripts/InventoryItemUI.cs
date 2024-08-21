using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI{
    public class InventoryItemUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image itemImage;
    
    [SerializeField] 
    TMP_Text itemQtyTxt;

    [SerializeField] 
    UnityEngine.UI.Image borderImg;

    public event Action<InventoryItemUI> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClicked;

    private bool empty = true;

    private void Awake() {
        ResetData();
        Deselect();
    }

    public void Deselect()
    {
        borderImg.enabled = false;
    }

    public void ResetData(){
        itemImage.gameObject.SetActive(false);
        empty = true;
    }
    
    public void SetData(Sprite sprite, int quantity){
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        itemQtyTxt.text = quantity + "";
        empty = false;
    }

    public void Select(){
        borderImg.enabled = true;
    }

    public void OnBeginDrag(){
        if (empty)
        {
            return;
        }
        // Debug.Log("OnBeginDrag triggered");
         OnItemBeginDrag?.Invoke(this);
    }
    public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }
    
    public void OnEndDrag(){
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerCLick(BaseEventData data){

        PointerEventData pointerData = (PointerEventData)data;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked.Invoke(this);
        }
    }



}

}