using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class BaseItem : IItem
    {
        private IItemData _data;
        public IItemData Data => _data;

        public BaseItem(IItemData data)
        {
            _data = data;
        }
    }

}
