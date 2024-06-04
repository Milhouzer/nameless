using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [System.Serializable]
    public class ItemSlot : IItemSlot
    {
        private int _index;
        public int Index => _index;

        private IItemStack _stack;

        public IItemStack Stack => _stack;

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
