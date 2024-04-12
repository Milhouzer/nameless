using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Milhouzer.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class InventoryBase : MonoBehaviour, IInventory<ItemSlot, IItemStack>
    {
        public bool IsEmpty => _slots.Count == 0;

        [SerializeField]
        private int _maxSlots;
        public int MaxSlots => _maxSlots;

        [SerializeField]
        private List<ItemStackDefinition> startItems = new();

        [SerializeField]
        private List<ItemSlot> _slots = new();
        public ReadOnlyCollection<ItemSlot> Slots => _slots.AsReadOnly();

        public event IInventory<ItemSlot, IItemStack>.AddItemEvent OnItemAdded;
        public event IInventory<ItemSlot, IItemStack>.RemoveItemEvent OnItemRemoved;

        InventoryRestrictions Restrictions;

        private void Awake() {

            Restrictions = GetComponent<InventoryRestrictions>();

            for(int i = 0; i < _maxSlots; i++)
            {
                ItemStack stack = new ItemStack(null, 0); 
                if(i < startItems.Count)
                {
                    stack = new ItemStack(startItems[i].Data, startItems[i].Amount);
                }
                _slots.Add(new ItemSlot(stack, i));
            }
            startItems.Clear();
        }

        /// <todo>
        /// Check result before sending event => if result is Added/RemovedNone, event shouldn't be sent.
        /// </todo>
        #region ITEM OPERATIONS

        public AddItemOperation AddItem(IItem item)
        {
            if(!CanAddItem(item))
                return AddItemOperation.AddedNone();

            ItemSlot slot = FindSlot(item);

            if(slot == null)
                return AddItemOperation.AddedNone();

            return AddItem(slot, item);
        }

        public AddItemOperation AddItem(IItemStack stack)
        {
            if(!CanAddItem(stack.Item))
                return AddItemOperation.AddedNone();

            ItemSlot slot = FindSlot(stack);
            
            if(slot == null)
                return AddItemOperation.AddedNone();

            return AddItem(slot, stack);
        }

        public AddItemOperation AddItem(ItemSlot slot, IItemStack stack)
        {

            if(!CanAddItem(stack.Item))
                return AddItemOperation.AddedNone();

            AddItemOperation operation = slot.Stack.Add(stack.Amount);
        
            ItemOperationEventData data = new ItemOperationEventData(slot, stack.Item, operation.Added);
            OnItemAdded?.Invoke(data);
            
            return operation;
        }

        public AddItemOperation AddItem(ItemSlot slot, IItem item)
        {
            if(!CanAddItem(item))
                return AddItemOperation.AddedNone();

            AddItemOperation operation = slot.Stack.Add(1);

            ItemOperationEventData data = new ItemOperationEventData(slot, item, operation.Added);
            OnItemAdded?.Invoke(data);

            return operation;
        }

        private bool CanAddItem(IItem item)
        {
            if(Restrictions != null && !Restrictions.SatisfyRestrictions(this, item.Data))
            {
                Debug.Log("Can't add item : " + item + " on " + transform);
                return false;
            }
            
            return true;
        }

        public RemoveItemOperation RemoveItem(IItem item)
        {
            ItemSlot slot = FindSlot(item);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation =  slot.Stack.Remove(1);

            ItemOperationEventData data = new ItemOperationEventData(slot, item, operation.Removed);
            OnItemRemoved?.Invoke(data);

            return operation;
        }

        public RemoveItemOperation RemoveItem(IItemStack stack)
        {
            ItemSlot slot = FindSlot(stack);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation = slot.Stack.Remove(stack.Amount);

            ItemOperationEventData data = new ItemOperationEventData(slot, stack.Item, operation.Removed);
            OnItemRemoved?.Invoke(data);

            return operation;
        }

        public RemoveItemOperation RemoveItem(ItemSlot slot)
        {
            RemoveItemOperation operation = slot.Stack.Remove(1);
            
            /// <TODO>
            /// REPLACE NULL
            /// </TODO>
            ItemOperationEventData data = new ItemOperationEventData(slot, null, operation.Removed);
            OnItemRemoved?.Invoke(data);

            return operation;
        }

        public RemoveItemOperation RemoveSlot(int i)
        {
            ItemSlot slot = _slots[i];
            
            /// <TODO>
            /// REPLACE NULL
            /// </TODO>
            ItemOperationEventData data = new ItemOperationEventData(slot, slot.Stack.Item, slot.Stack.Amount);
            OnItemRemoved?.Invoke(data);

            int removed = slot.Stack.Amount;
            _slots.RemoveAt(i);

            return new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, removed);
        }


            
        #endregion

        
        public GameObject DropItem(IItem item)
        {
            ItemSlot slot = FindSlot(item);
            Vector3 dropPos = gameObject.transform.position + transform.forward;
            
            GameObject droppedItem = InventoryManager.DropItem(slot.Stack, dropPos);

            RemoveItem(slot);

            return droppedItem;
        }

        public GameObject DropItem(IItemStack stack)
        {
            ItemSlot slot = FindSlot(stack);
            
            Vector3 dropPos = gameObject.transform.position + transform.forward;
            
            GameObject droppedItem = InventoryManager.DropItem(slot.Stack, dropPos);

            RemoveItem(slot);

            return droppedItem;
        }


        public ItemSlot FindSlot(IItemStack stack)
        {
            return FindSlot(stack?.Item);
        }

        public ItemSlot FindSlot(IItem item)
        {
            if(item == null || item.Data == null)
                return null;

            ItemSlot firstEmpty = null;
            int index = 0;

            for(int i = 0; i < Slots.Count; i++)
            {
                ItemSlot slot = Slots[i];
                if(slot.Data == null)
                {
                    if(firstEmpty == null)
                    {
                        firstEmpty = slot;
                        index = i;
                    }
                }else
                {
                    if(item.Data.ID == slot.Stack.Item.Data.ID)
                        return slot;
                }
            }

            // Update item stack on slot.
            _slots[index] = new ItemSlot(new ItemStack(item.Data, 0), index);
            
            return _slots[index];
        }

        /// <TODO>
        /// Delete
        /// </TODO>
        /// <returns></returns>
        public string ListItems()
        {
            StringBuilder builder = new StringBuilder();
            foreach(ItemSlot slot in _slots)
            {
                if(slot.Data == null)
                    continue;

                builder.AppendLine(slot.Index.ToString() + " : " + slot.Data.ID + ", " + slot.Stack.Amount);
            }

            return builder.ToString();
        }

        public ItemSlot FindItem(Predicate<ItemSlot> predicate)
        {
            return _slots.Find(predicate);
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
