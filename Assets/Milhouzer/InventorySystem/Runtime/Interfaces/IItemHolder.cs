using System.Collections.Generic;


namespace Milhouzer.InventorySystem
{
    /// <summary>
    /// Lightweight inventory
    /// </summary>
    /// <TODO>
    /// Delete this garbage
    /// </TODO>
    public interface IItemHolder
    {
        bool HoldSingleItem { get; }
        int Capacity { get; }
        List<IItemStack> Items { get; }
        
        AddItemOperation Hold(IItemStack item);
        AddItemOperation Hold(List<IItemStack> items);
        RemoveItemOperation Pickup(IInventory inventory);

        public delegate void PickUpEvent(/*ItemOperationEventData eventData*/);
        public event PickUpEvent OnPickedUp;
    }
}
