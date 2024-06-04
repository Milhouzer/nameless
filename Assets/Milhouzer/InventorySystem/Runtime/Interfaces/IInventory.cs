using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IInventory : IUIDataSerializer
    {
        ////// PROPERTIES //////
        
        public bool IsEmpty { get; }
        public int MaxSlots { get; }
        public List<IItemSlot> Slots { get; }


        ////// METHODS //////
        
        public AddItemOperation AddItem(IItemStack stack);

        public RemoveItemOperation RemoveItem(IItem item);
        public RemoveItemOperation RemoveItem(IItem item, int amount);

        // public GameObject DropItem(IItem item);
        // public GameObject DropItem(TStack stack);


        ////// EVENTS //////
        delegate void AddItemEvent(ItemOperationEventData eventData);
        delegate void RemoveItemEvent(ItemOperationEventData eventData);

        event AddItemEvent OnItemAdded;
        event RemoveItemEvent OnItemRemoved;
    }

    public struct ItemOperationEventData
    {
        public ItemOperationEventData(IItemSlot slot, IItem item, int amount)
        {
            Slot = slot;
            Item = item;
            Amount = amount;
        }

        public IItemSlot Slot { get; private set; }
        public IItem Item { get; private set; }
        public int Amount { get; private set; }
    }
}