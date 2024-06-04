using System.Collections;
using System.Collections.Generic;
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
        public BaseItem Item => _item;

        private int _amount;
        public int Amount => _amount;

        public int MaxAmount => _item.Data.IsStackable ? _item.Data.MaxStack : 1;

        public bool IsFull => Amount == MaxAmount;
        public bool IsEmpty => Amount == 0;

        public ItemStack(IItemData data, int amount)
        {
            if(data == null)
            {
                return;
            }

            _item = new BaseItem(data);
            _amount = Mathf.Clamp(amount, 0, data.MaxStack);
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
            
            return new AddItemOperation(added == amount ? AddItemOperationResult.AddedAll : AddItemOperationResult.PartiallyAdded, added);
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

            return new RemoveItemOperation(removed == amount ? RemoveItemOperationResult.RemovedAll : RemoveItemOperationResult.PartiallyRemoved, removed);
        }
    }
}
