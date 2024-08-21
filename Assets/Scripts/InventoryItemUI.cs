using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image itemImage;
    
    [SerializeField] 
    TMP_Text itemQtyTxt;

    [SerializeField] 
    UnityEngine.UI.Image borderImg;

    public event Action<InventoryItemUI> OnItemClicked, OnItemDroppedOn, OnItemBeingDrag, OnItemEndDrag, OnRightMouseBtnClicked;

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

    public void OnBeingDrag(){
        if (empty)
        {
            return;
        }OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrop(){
        OnItemDroppedOn?.Invoke(this);
    }
    public void OnEndDrag(){
        OnItemDroppedOn.Invoke(this);
    }

    public void OnPointerCLick(BaseEventData data){

        if (empty)
        {
            return;
        }

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
