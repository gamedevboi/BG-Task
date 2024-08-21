using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



namespace Inventory.Model{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }


        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty){
                    inventoryItems[i] = new InventoryItem{
                        item = item, quantity = quantity
                    };
                    return;
                }
                
            }
        }
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIdx)
        {
            return inventoryItems[itemIdx];
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public void SwapItems(int itemIdx_1, int itemIdx_2)
        {
            InventoryItem item1 = inventoryItems[itemIdx_1];
            inventoryItems[itemIdx_1] = inventoryItems[itemIdx_2];
            inventoryItems[itemIdx_2] = item1;
            InformABoutChanges();
        }

        private void InformABoutChanges()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    
    
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        // public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                // itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
                // itemState = new List<ItemParameter>()
            };
    }

}