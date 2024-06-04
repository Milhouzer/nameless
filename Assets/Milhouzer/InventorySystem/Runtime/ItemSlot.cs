using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [System.Serializable]
    public class ItemSlot : ISlot
    {
        private int _index;
        public int Index => _index;

        private IItemStack _stack;

        public IItemStack Stack => _stack;

        /// <summary>
        /// Get Stack.Item.Data if stack or item is not null;
        /// </summary>
        /// <value></value>
        public IItemData Data 
        {
            get {
                if(_stack == null)
                    return null;
                if(_stack.Item == null)
                    return null;

                return _stack.Item.Data;
            }
        }

        /// <summary>
        /// Wrapper for _stack.Item
        /// </summary>
        /// <value></value>
        public IItem Item
        {
            get {
                if(_stack == null)
                    return null;

                return _stack.Item;
            }
        }

        public ItemSlot(IItemStack stack, int index)
        {
            Debug.Log("Created slot with index " + index);
            _index = index;
            _stack = stack;
        }
    }
}
