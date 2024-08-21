using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


        public int AddItem(ItemSO item, int quantity)
        {

            if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemTOFirstFreeSlot(item, 1);
                    }
                    InformABoutChanges();
                    return quantity;
                   
                    
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformABoutChanges();
            return quantity;

            
        }

        private int AddItemTOFirstFreeSlot(ItemSO item, int qty)
        {
            InventoryItem newItem = new InventoryItem{item = item, quantity = qty};
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return qty;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() 
        => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].IsEmpty){
                    continue;
                }
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleTOTake = inventoryItems[i].item.MaxStackSize- inventoryItems[i].quantity;

                    if (quantity > amountPossibleTOTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleTOTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformABoutChanges();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQty = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQty;
                AddItemTOFirstFreeSlot(item, newQty);
            }
            return quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);

                InformABoutChanges();
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