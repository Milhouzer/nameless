using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Milhouzer.Core.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    [System.Serializable]
    public class ItemSlot : IItemSlot
    {
        private int _index;
        public int Index => _index;

        private IItemStack _stack;

        public IItemStack Stack => _stack;

        public InventoryRestrictions Restrictions;

        /// <summary>
        /// Wrapper for _stack.Item
        /// </summary>
        /// <value></value>
        public IItem Item
        {
            get {
                return _stack.Item;
            }
        }

        public ItemSlot(IItemStack stack, int index, InventoryRestrictions restrictions = null)
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));
            
            _index = index;
            _stack = stack;
            Restrictions = restrictions;
            if(Item == null)
            {
                Debug.LogWarning("Item is null");
            }else{
                Debug.Log($"created slot: {_index} containing {stack.Item.Data.ID} with restrictions {restrictions}");
            }
        }

        public ItemSlot(IItemStack stack, int index) : this(stack, index, null) {}
       
        public bool CanAddItem(IItem item)
        {
            if(item == null)
                return true;
                
            if(Restrictions == null)
            {
            // TODO: check if item can stack with _stack.Item
                Debug.Log("Restrictions null : " + _stack.Item);
                return _stack.Item == null;
            }
            Debug.Log(item + " : " + Restrictions.SatisfyRestrictions(item.Data) );
            return Restrictions.SatisfyRestrictions(item.Data);
        }

        public void Empty()
        {
            _stack.Remove(_stack.Amount);
        }

        public bool IsEmpty()
        {
            return Item == null;
        }
    }
}
