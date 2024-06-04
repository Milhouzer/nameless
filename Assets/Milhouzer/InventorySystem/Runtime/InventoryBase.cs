using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Milhouzer.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class InventoryBase : MonoBehaviour, IInventory
    {
        public bool IsEmpty => _slots.Count == 0;

        [SerializeField]
        private int _maxSlots;
        public int MaxSlots => _maxSlots;

        [SerializeField]
        private List<ItemStackDefinition> startItems = new();

        [SerializeField]
        private List<IItemSlot> _slots = new();
        public List<IItemSlot> Slots => _slots;

        public event IInventory.AddItemEvent OnItemAdded;
        public event IInventory.RemoveItemEvent OnItemRemoved;

        InventoryRestrictions Restrictions;

        private void Awake() {

            Restrictions = GetComponent<InventoryRestrictions>();

            for(int i = 0; i < startItems.Count; i++)
            {
                ItemStack stack = new ItemStack(startItems[i].Data, startItems[i].Amount);
                _slots.Add(new ItemSlot(stack, i));
            }
            startItems.Clear();
        }

        /// <todo>
        /// Check result before sending event => if result is Added/RemovedNone, event shouldn't be sent.
        /// </todo>
        #region ITEM OPERATIONS

        // private AddItemOperation AddItem(IItem item)
        // {
        //     if(item == null)
        //     {
        //         Debug.LogError("Item was null");
        //         return AddItemOperation.AddedNone();
        //     }

        //     if(!CanAddItem(item))
        //         return AddItemOperation.AddedNone();

        //     ItemSlot slot = this.FindSlot(item);
        //     return AddItem(slot, item);
        // }

        public AddItemOperation AddItem(IItemStack stack)
        {
            if(stack.Item == null)
            {
                Debug.LogError("Item was null");
                return AddItemOperation.AddedNone();
            }

            if(!CanAddItem(stack.Item))
                return AddItemOperation.AddedNone();

            IItemSlot slot = this.FindSlot(stack);
            return AddItem(slot, stack);
        }

        // private AddItemOperation AddItem(IItemSlot slot, IItem item)
        // {
        //     if(item == null)
        //     {
        //         Debug.LogError("Item was null");
        //         return AddItemOperation.AddedNone();
        //     }

        //     if(slot == null)
        //         slot = CreateSlot(item);
                
        //     if(slot == null || !CanAddItem(item))
        //         return AddItemOperation.AddedNone();

        //     AddItemOperation operation = slot.Stack.Add(1);

        //     ItemOperationEventData data = new ItemOperationEventData(slot, item, operation.Added);
        //     OnItemAdded?.Invoke(data);

        //     return operation;
        // }

        private AddItemOperation AddItem(IItemSlot slot, IItemStack stack)
        {
            if(stack.Item == null)
            {
                Debug.LogError("Item was null");
                return AddItemOperation.AddedNone();
            }

            if(slot == null)
                slot = CreateSlot(stack.Item);
                
            if(slot == null || !CanAddItem(stack.Item))
                return AddItemOperation.AddedNone();

            AddItemOperation operation = slot.Stack.Add(stack.Amount);
        
            ItemOperationEventData data = new ItemOperationEventData(slot, stack.Item, operation.Added);
            OnItemAdded?.Invoke(data);
            
            return operation;
        }

        private ItemSlot CreateSlot(IItem item)
        {
            if(_slots.Count < MaxSlots)
            {
                ItemSlot slot =  new ItemSlot(new ItemStack(item.Data, 0), _slots.Count);
                _slots.Add(slot);
                return slot;
            }

            return null;
        }

        // private ItemSlot CreateSlot(IItemStack stack)
        // {
        //     if(_slots.Count < MaxSlots)
        //     {
        //         ItemSlot slot = new ItemSlot(stack, _slots.Count - 1);
        //         _slots.Add(slot);
        //         return slot;
        //     }

        //     return null;
        // }

        private bool CanAddItem(IItem item)
        {
            if(item == null || Restrictions != null && !Restrictions.SatisfyRestrictions(this, item.Data))
            {
                Debug.Log("Can't add item : " + item + " on " + transform);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Remove item completely
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>//
        public RemoveItemOperation RemoveItem(IItem item)
        {
            IItemSlot slot = this.FindSlot(item);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation =  new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, slot.Stack.Amount);
            ItemOperationEventData data = new ItemOperationEventData(slot, item, slot.Stack.Amount);

            _slots.Remove(slot);

            OnItemRemoved?.Invoke(data);
            return operation;
        }

        public RemoveItemOperation RemoveItem(IItem item, int amount)
        {
            IItemSlot slot = this.FindSlot(item);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation =  slot.Stack.Remove(1);

            ItemOperationEventData data = new ItemOperationEventData(slot, item, operation.Removed);
            OnItemRemoved?.Invoke(data);

            return operation;
        }

        /// <summary>
        /// Remove item completely from given slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        private RemoveItemOperation RemoveItem(IItemSlot slot)
        {
            RemoveItemOperation operation = slot.Stack.Remove(1);
            
            /// <TODO>
            /// REPLACE NULL. However this implies to store the item before removing it...
            /// </TODO>
            ItemOperationEventData data = new ItemOperationEventData(slot, null, operation.Removed);
            OnItemRemoved?.Invoke(data);

            return operation;
        }

        /// <summary>
        /// Remove item at slot i
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private RemoveItemOperation RemoveSlot(int i)
        {
            IItemSlot slot = _slots[i];
            
            /// <TODO>
            /// REPLACE NULL
            /// </TODO>
            ItemOperationEventData data = new ItemOperationEventData(slot, slot.Item, slot.Stack.Amount);
            OnItemRemoved?.Invoke(data);

            int removed = slot.Stack.Amount;
            _slots.RemoveAt(i);

            return new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, removed);
        }


            
        #endregion

        
        // public GameObject DropItem(IItem item)
        // {
        //     ItemSlot slot = this.FindSlot(item);
        //     Vector3 dropPos = gameObject.transform.position + transform.forward;
            
        //     GameObject droppedItem = InventoryManager.DropItem(slot.Stack, dropPos);

        //     RemoveItem(slot);

        //     return droppedItem;
        // }

        // public GameObject DropItem(IItemStack stack)
        // {
        //     ItemSlot slot = this.FindSlot(stack);
            
        //     Vector3 dropPos = gameObject.transform.position + transform.forward;
            
        //     GameObject droppedItem = InventoryManager.DropItem(slot.Stack, dropPos);

        //     RemoveItem(slot);

        //     return droppedItem;
        // }

        /// <TODO>
        /// Delete
        /// </TODO>
        /// <returns></returns>
        public string ListItems()
        {
            StringBuilder builder = new StringBuilder();
            foreach(ItemSlot slot in _slots)
            {
                if(slot.Item.Data == null)
                    continue;

                builder.AppendLine(slot.Index.ToString() + " : " + slot.Item.Data.ID + ", " + slot.Stack.Amount);
            }

            return builder.ToString();
        }

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Type","Inventory"},
                {"Slots", Slots},
            };
        }
    }
}
