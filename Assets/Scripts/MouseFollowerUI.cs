using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerUI : MonoBehaviour
{
   [SerializeField]
   private Canvas canvas;
   

   [SerializeField]
   private InventoryItemUI item;


   public void Awake() {
    canvas = transform.root.GetComponent<Canvas>();
    item = GetComponentInChildren<InventoryItemUI>();
   }

   public void SetData(Sprite sprite, int qty){
    item.SetData(sprite,qty);

   }

   private void Update() {
    Vector2 pos;
    RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out pos);
    transform.position = canvas.transform.TransformPoint(pos);
    
   }

   public void Toggle(bool value){
    gameObject.SetActive(value);
   }
}
