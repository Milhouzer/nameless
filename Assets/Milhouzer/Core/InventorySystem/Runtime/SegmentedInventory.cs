
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    public class SegmentedInventory : InventoryBase
    {
        public new event IInventory.AddItemEvent OnItemAdded;
        public new event IInventory.RemoveItemEvent OnItemRemoved;
        
        List<InventorySegmentation> segmentations = new();

        protected override void Awake()
        {
            if(dataInjector is SegmentedInventoryDataInjector injector)
            {
                segmentations = injector.segmentations;
                int index = 0;
                foreach(InventorySegmentation segmentation in segmentations)
                {
                    Debug.Log($"{segmentation.Name} added to {this}: {segmentation.Index} to {segmentation.Index + segmentation.Length - 1}");
                    for(int i = 0; i < segmentation.Length; i++){
                        if(index < injector.SlotsData.Count){
                            SlotData data = injector.SlotsData[index];
                            ItemStack stack = new ItemStack(data.ItemData, data.Amount);
                            _slots.Add(new ItemSlot(stack, index, data.Restrictions));
                        }
                    }
                    index++;
                }
                Debug.Log(_slots.Count);
            }
            else{
                base.Awake();
            }
        }

        public override AddItemOperation AddItem(IItemStack stack, string name = "")
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            Debug.Log($"add item: try add {stack.Item.Data.DisplayName} to {name}");
            List<IItemSlot> subInventory = GetInventory_Internal(name);
            if(subInventory == null)
            {
                Debug.Log($"add item: couldn't find {name}");
                return AddItemOperation.AddedNone();
            }

            if(!CanAddItem(stack.Item, subInventory, out IItemSlot slot))
            {
                Debug.Log($"can not add item: couldn't add {stack.Item.Data.DisplayName} to {gameObject.name}/{name}");
                return AddItemOperation.AddedNone();
            }

            if(slot.Stack.Item == null)
            {
                _slots[slot.Index] = new ItemSlot(new ItemStack(stack.Item.Data, stack.Amount), slot.Index);
                AddItemOperation operation = new AddItemOperation(AddItemOperationResult.AddedAll, stack.Item, stack.Amount);
                OnItemAdded?.Invoke(operation);
                return operation;
            }else
            {
                AddItemOperation operation = slot.Stack.Add(stack.Amount);
                OnItemAdded?.Invoke(operation);
                return operation;
            }
            
        }
        /// <summary>
        /// Check if item can be added to the inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static bool CanAddItem(IItem item, List<IItemSlot> slots, out IItemSlot slot)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Debug.Log(slots.Count);

            slot = null;
            foreach(IItemSlot _slot in slots){
                
                if(_slot.CanAddItem(item))
                {
                    slot = _slot;
                    return true;
                }
            }

            return false;
        }

        public override RemoveItemOperation RemoveItem(IItem item, int amount, string name = "")
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            List<IItemSlot> subInventory = GetInventory_Internal(name);
            if(subInventory == null)
                return RemoveItemOperation.RemovedNone();

            IItemSlot slot = subInventory.FindItemSlot(x => x.Item.Data.ID == item.Data.ID);
            if(slot == null)
                return RemoveItemOperation.RemovedNone();

            if(amount < slot.Stack.Amount)
            {
                slot.Stack.Remove(amount);
                RemoveItemOperation op = new RemoveItemOperation(RemoveItemOperationResult.PartiallyRemoved, item, amount);
                OnItemRemoved?.Invoke(op);
                return op;
            }
            else
            {
                int removed = slot.Stack.Amount;
                
                slot.Empty();
                RemoveItemOperation op = new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, item, removed);
                OnItemRemoved?.Invoke(op);
                return op;
            }
        }

        public override RemoveItemOperation RemoveItem(IItem item, string name = "")
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            List<IItemSlot> subInventory = GetInventory_Internal(name);
            if(subInventory == null)
                return base.RemoveItem(item, name);

            IItemSlot slot = subInventory.FindItemSlot(x => x.Item.Data.ID == item.Data.ID);
            if(slot == null)
                return RemoveItemOperation.RemovedNone();


            int removed = slot.Stack.Amount;
            slot.Empty();
            RemoveItemOperation op = new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, item, removed);
            OnItemRemoved?.Invoke(op);
            
            return op;
        }
        

        /// <summary>
        /// Remove item completely from given slot in specified inventory.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public override RemoveItemOperation RemoveItem(int i, string name = "")
        {
            List<IItemSlot> subInventory = GetInventory_Internal(name);

            if(i > subInventory.Count)
                throw new IndexOutOfRangeException(nameof(i));
            
            IItemSlot slot = subInventory[i];
            int removed = slot.Stack.Amount;

            RemoveItemOperation operation = new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, slot.Item, removed);
            OnItemRemoved?.Invoke(operation);
            slot.Stack.Remove(slot.Stack.Amount);

            return operation;
        }

        /// <summary>
        /// Remove item completely from given slot in specified inventory.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public override RemoveItemOperation RemoveItem(int i, int amount, string name = "")
        {
            List<IItemSlot> subInventory = GetInventory_Internal(name);

            if(i > subInventory.Count)
                throw new IndexOutOfRangeException(nameof(i));
            
            IItemSlot slot = subInventory[i];
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

        public override ReadOnlyCollection<IItemSlot> GetInventory(string name)
        {
            return GetInventory_Internal(name)?.AsReadOnly();
        }

        private List<IItemSlot> GetInventory_Internal(string name)
        {
            InventorySegmentation segment = GetInventorySegementation(name);
            if(ReferenceEquals(segment, default(InventorySegmentation)))
            {
                Debug.LogWarning($"no inventory named {name}");
                return new List<IItemSlot>();
            }

            try
            {
                return _slots.GetRange(segment.Index, segment.Length);
            }
            catch (System.Exception)
            {
                Debug.Log($"Counldn't get slots range: {segment.Index}, {segment.Index + segment.Length - 1}, {_slots.Count}");
            }

            return new List<IItemSlot>();
        }

        protected virtual InventorySegmentation GetInventorySegementation(string name)
        {
            foreach(InventorySegmentation segmentation in segmentations)
            {
                Debug.Log($"find {name}: {segmentation.Name}");
                if(segmentation.Name == name){
                    Debug.Log($"found {name}: {segmentation.Name}");
                    return segmentation;
                }
            }
            return segmentations.Find(x => x.Name == name);
        }
            
        public override Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel","Inventory"},
                {"Slots", Slots},
                {"Inventory", this},
            };
        }
    }
}