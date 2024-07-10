using System;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [Obsolete]
    public class DroppedItem : MonoBehaviour, IItemHolder
    {
        private bool _holdSingleItem = true;
        public bool HoldSingleItem => _holdSingleItem;
        private int _capacity = 1;
        public int Capacity => _capacity;

        public List<IItemStack> _items = new();
        public List<IItemStack> Items => _items;

        public event IItemHolder.PickUpEvent OnPickedUp;

        public AddItemOperation Hold(IItemStack stack)
        {
            if(_holdSingleItem && _items.Count != 0)
            {
                return AddItemOperation.AddedNone();
            }
            _items.Add(stack);

            return new AddItemOperation(AddItemOperationResult.AddedAll, stack.Item, stack.Amount);
            
        }

        public AddItemOperation Hold(List<IItemStack> items)
        {
            if(_holdSingleItem && _items.Count != 0)
            {
                return AddItemOperation.AddedNone();
            }

            _items.AddRange(items);
            return new AddItemOperation(AddItemOperationResult.AddedAll, items[0].Item, items[0].Amount);
        }

        public RemoveItemOperation Pickup(IInventory inventory)
        {
            AddItemOperation operation = inventory.AddItem(_items[0]);

            Destroy(gameObject);

            OnPickedUp?.Invoke();
            return new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, null, operation.Added);
        }
    }
}
