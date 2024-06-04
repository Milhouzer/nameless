using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IInventory<TSlot, TStack> : IUIDataSerializer
     where TSlot : ISlot 
     where TStack : IItemStack
    {
        ////// PROPERTIES //////
        
        public bool IsEmpty { get; }
        public int MaxSlots { get; }
        public List<TSlot> Slots { get; }


        ////// METHODS //////
        
        public AddItemOperation AddItem(IItem item);
        public AddItemOperation AddItem(TStack stack);
        public AddItemOperation AddItem(TSlot slot, TStack stack);
        public AddItemOperation AddItem(TSlot slot, IItem item);

        public RemoveItemOperation RemoveItem(IItem item);
        public RemoveItemOperation RemoveItem(IItem item, int amount);

        public GameObject DropItem(IItem item);
        public GameObject DropItem(TStack stack);


        ////// EVENTS //////
        delegate void AddItemEvent(ItemOperationEventData eventData);
        delegate void RemoveItemEvent(ItemOperationEventData eventData);

        event AddItemEvent OnItemAdded;
        event RemoveItemEvent OnItemRemoved;
    }

    public struct ItemOperationEventData
    {
        public ItemOperationEventData(ISlot slot, IItem item, int amount)
        {
            Slot = slot;
            Item = item;
            Amount = amount;
        }

        public ISlot Slot { get; private set; }
        public IItem Item { get; private set; }
        public int Amount { get; private set; }
    }
}