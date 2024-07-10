using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class InventoryBase : MonoBehaviour, IInventory
    {
        public bool IsEmpty => _slots.Count == 0;

        [SerializeField]
        protected int _maxSlots;
        public int MaxSlots => _maxSlots;

        [SerializeField]
        protected List<IItemSlot> _slots = new();
        public List<IItemSlot> Slots => _slots;

        [SerializeField]
        protected InventoryDataInjector dataInjector;

        public event IInventory.AddItemEvent OnItemAdded;
        public event IInventory.RemoveItemEvent OnItemRemoved;

        protected virtual void Awake() 
        {
            if(dataInjector != null)
            {
                for(int i = 0; i < dataInjector.SlotsData.Count; i++){
                    SlotData data = dataInjector.SlotsData[i];
                    ItemStack stack = data.ItemData == null ? null : new ItemStack(data.ItemData, data.Amount);
                    _slots.Add(new ItemSlot(stack, i, stack == null ? null : data.Restrictions));
                }
            }
        }

        #region INVENTORY SPLITTING
        
        /// <summary>
        /// Inventory base is only made of a single inventory (i.e. all items are stored in the same container).
        /// Thus this method always return all the slots in <see cref="_slots"/>
        /// </summary>
        /// <param name="name">name of the inventory to get.</param>
        /// <returns></returns>
        public virtual ReadOnlyCollection<IItemSlot> GetInventory(string name)
        {
            return _slots.AsReadOnly();
        }

        #endregion



        /// <TODO>
        /// Check result before sending event => if result is Added/RemovedNone, event shouldn't be sent.
        /// </TODO>
        #region ADD ITEMS

        public virtual AddItemOperation AddItem(IItemStack stack, string name = "")
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            if(!CanAddItem(stack.Item, out IItemSlot slot)){

                Debug.Log($"can not add item: couldn't add {stack.Item.Data.DisplayName} to {gameObject.name}");
                return AddItemOperation.AddedNone();
            }

            AddItemOperation operation = slot.Stack.Add(stack.Amount);
            OnItemAdded?.Invoke(operation);
            return operation;
        }

        /// <summary>
        /// Check if item can be added to the inventory.
        /// First check if the inventory is full. If so, return false
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool CanAddItem(IItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            foreach(IItemSlot slot in _slots){
                if(slot.CanAddItem(item))
                    return true;

                Debug.Log($"can not add item: couldn't add {item} to {slot}");
            }
            
            return false;
        }

        /// <summary>
        /// Check if item can be added to the inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal virtual bool CanAddItem(IItem item, out IItemSlot slot)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            slot = null;
            foreach(IItemSlot _slot in _slots){
                
                if(_slot.CanAddItem(item))
                {
                    slot = _slot;
                    return true;
                }
            }
            Debug.Log($"can not add item: couldn't add {item.Data.DisplayName} to {this}");

            return false;
        }

        protected virtual ItemSlot CreateSlot(IItem item)
        {
            if(_slots.Count < _maxSlots)
            {
                ItemSlot slot =  new ItemSlot(new ItemStack(item.Data, 0), _slots.Count);
                _slots.Add(slot);
                return slot;
            }

            return null;
        }

        #endregion

        /// <TODO>
        /// Check result before sending event => if result is Added/RemovedNone, event shouldn't be sent.
        /// </TODO>
        #region REMOVE ITEMS

        public virtual RemoveItemOperation RemoveItem(IItem item, string name = "")
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            IItemSlot slot = this.FindSlot(item);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation =  new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, item, slot.Stack.Amount);

            _slots.Remove(slot);

            OnItemRemoved?.Invoke(operation);
            return operation;
        }

        public virtual RemoveItemOperation RemoveItem(IItem item, int amount, string name = "")
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            IItemSlot slot = this.FindSlot(item);
            
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            RemoveItemOperation operation =  slot.Stack.Remove(amount);
            OnItemRemoved?.Invoke(operation);

            return operation;
        }

        /// <summary>
        /// Remove item completely from given slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public virtual RemoveItemOperation RemoveItem(int i, string name = "")
        {
            if(i > _slots.Count)
                throw new IndexOutOfRangeException(nameof(i));
            
            int removed = _slots[i].Stack.Amount;

            RemoveItemOperation operation = new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, _slots[i].Item, removed);
            OnItemRemoved?.Invoke(operation);
            _slots[i].Stack.Remove(_slots[i].Stack.Amount);

            return operation;
        }

        /// <summary>
        /// Remove item completely from given slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public virtual RemoveItemOperation RemoveItem(int i, int amount, string name = "")
        {
            if(i > _slots.Count)
                throw new IndexOutOfRangeException(nameof(i));
            
            IItemSlot slot = _slots[i];
            int removed = Mathf.Min(amount, slot.Stack.Amount);

            RemoveItemOperation operation = new RemoveItemOperation(
                removed == amount ? RemoveItemOperationResult.RemovedAll : RemoveItemOperationResult.PartiallyRemoved,
                slot.Item,
                removed
            );
            
            OnItemRemoved?.Invoke(operation);
            slot.Stack.Remove(amount);

            return operation;
        }

        #endregion

        #region UI DATA SERIALIZER
            
        public virtual Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel","Inventory"},
                {"Slots", Slots},
                {"Inventory", this},
            };
        }

        #endregion
    }
}
