using System;

namespace Milhouzer.InventorySystem
{
    public class BaseItem : IItem
    {
        private IItemData _data;
        public IItemData Data => _data;

        public BaseItem(IItemData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            _data = data;
        }
    }

}
