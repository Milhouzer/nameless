using System;
using UnityEngine;

namespace Milhouzer.InventorySystem
{

    public enum AddItemOperationResult
    {
        AddedAll,
        PartiallyAdded,
        AddedNone,
    }
    public enum RemoveItemOperationResult
    {
        RemovedAll,
        PartiallyRemoved,
        RemovedNone,
    }

    [System.Serializable]
    public struct ItemStackDefinition
    {
        public BaseItemData Data;
        [Range(1, 100)]
        public int Amount;
    }

    public class ItemStack : IItemStack
    {
        BaseItem _item;
        public IItem Item => _item;

        private int _amount;
        public int Amount => _amount;

        public int MaxAmount => _item.Data.IsStackable ? _item.Data.MaxStack : 1;

        public bool IsFull => Amount == MaxAmount;
        public bool IsEmpty => Amount == 0;

        public ItemStack(IItemData data, int amount)
        {
            // if (data == null) throw new ArgumentNullException(nameof(data));
            if(data == null)
                Debug.LogWarning("Created item with null data");
            else   
                _item = new BaseItem(data);
                
            _amount = Mathf.Clamp(amount, 0, data == null ? int.MaxValue : data.MaxStack);
        }

        public AddItemOperation Add(int amount)
        {
            if(IsFull)
            {
                return AddItemOperation.AddedNone();
            }

            int added = Mathf.Min(MaxAmount - Amount, amount);
            Debug.Log("Add item " + amount + " " + added + " " + MaxAmount);
            _amount += added;
            
            return new AddItemOperation(added == amount ? AddItemOperationResult.AddedAll : AddItemOperationResult.PartiallyAdded, _item, added);
        }

        public RemoveItemOperation Remove(int amount)
        {

            if(IsEmpty)
            {
                return RemoveItemOperation.RemovedNone();
            }

            int removed = Mathf.Min(_amount, amount);
            Debug.Log(removed + " " + _amount + " " + amount);
            _amount -= removed;

            RemoveItemOperation result = new RemoveItemOperation(
                removed == amount ? RemoveItemOperationResult.RemovedAll : RemoveItemOperationResult.PartiallyRemoved,
                _item,
                removed
            );

            if(_amount == 0)
                _item = null;

            return result;
        }
    }
}
