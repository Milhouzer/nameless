using System.Collections.Generic;


namespace Milhouzer.InventorySystem
{
    public interface IItemHolder
    {
        bool HoldSingleItem { get; }
        int Capacity { get; }
        List<IItemStack> Items { get; }
        
        AddItemOperation Hold(IItemStack item);
        AddItemOperation Hold(List<IItemStack> items);
        RemoveItemOperation Pickup(InventoryBase inventory);

        public delegate void PickUpEvent(/*ItemOperationEventData eventData*/);
        public event PickUpEvent OnPickedUp;
    }
}
