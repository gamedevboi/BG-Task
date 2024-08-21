using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptionUI : MonoBehaviour
{
   [SerializeField]
   private Image itemImg;

   [SerializeField]
   private TMP_Text itemTitleTxt;

   [SerializeField]
   private TMP_Text itemDescTxt;


   public void Awake() {
    ResetDescription();
   }

    public void ResetDescription()
    {
        itemImg.gameObject.SetActive(true);
        itemTitleTxt.text = "";
        itemDescTxt.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription){
        itemImg.gameObject.SetActive(true);
        itemDescTxt.text = itemDescription;
        itemTitleTxt.text = itemName;
        itemImg.sprite = sprite;
    }
}
