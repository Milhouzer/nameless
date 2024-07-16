using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.Core.InventorySystem
{
    /// <summary>
    /// Inventories can be splitted into smaller sections. Sections are defined by name, for example:
    /// A player inventory can be splitted into equipment, inventory, tools, etc.
    /// A furnace inventory can be splitted into input, fuel and output.
    /// etc.
    /// 
    /// This produces a quite more complex but a convenient way to store different inventory parts on a single entity.
    /// </summary>
    /// <remarks>
    /// Inventories should be coded this way : RemoveItem operations are not subjects to restrictions contrary to AddItem operations.
    /// This allows to use the AddItem operation as a confirmation to the RemoveItem operation (using the RemoveItemOperation returned).
    /// This way there is not real Transfer method, only a succession of deletions and additions.
    /// </remarks>
    /// <TODO>
    /// Split into smaller interfaces
    /// Implement GetEnumerator on slots
    /// /!\ THIS SHOULD NOT IMPLEMENT <see cref="IUIDataSerializer"/> /!\ 
    /// </TODO>
    public interface IInventory : IUIDataSerializer
    {
        ////// PROPERTIES //////
        
        /// <summary>
        /// Is the inventory empty
        /// </summary>
        /// <value></value>
        public bool IsEmpty { get; }

        /// <summary>
        /// Max slots the inventory can hold
        /// </summary>
        /// <value></value>
        public int MaxSlots { get; }

        /// <summary>
        /// Inventory slots
        /// </summary>
        /// <value></value>
        public List<IItemSlot> Slots { get; }

        /// <summary>
        /// Restrictions of the inventory. When trying to add an items, these restrictions should be checked to see if the item can be added.
        /// </summary>
        /// <value></value>
        // public InventoryRestrictions Restrictions { get; }

        ////// METHODS //////
        
        /// <summary>
        /// Add a <see cref="IItemStack"/> to the inventory
        /// </summary>
        /// <param name="stack">Item stack to add</param>
        /// <returns>Operation done</returns>
        public AddItemOperation AddItem(IItemStack stack, string name = "");

        /// <summary>
        /// Remove an item from the inventory completely
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>Operation done</returns>
        public RemoveItemOperation RemoveItem(IItem item, string name = "");

        /// <summary>
        /// Remove a given amount of an item from an inventory
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="amount">Amount of the item to remove</param>
        /// <returns>Operation done</returns>
        /// <TODO>
        /// Replace <paramref name="item"/> by int index to avoid ?
        /// </TODO>
        public RemoveItemOperation RemoveItem(IItem item, int amount, string name = "");

        /// <summary>
        /// Remove item at index <paramref name="i"/> of inventory defined by <paramref name="name"/>
        /// i should already be offseted to the corresponding section.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public RemoveItemOperation RemoveItem(int i, string name = "");

        /// <summary>
        /// Remove item at index <paramref name="i"/> of inventory defined by <paramref name="name"/>
        /// i should already be offseted to the corresponding section.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public RemoveItemOperation RemoveItem(int i, int amount, string name = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IItemSlot> GetInventory(string name = "");

        ////// EVENTS //////
        delegate void AddItemEvent(AddItemOperation eventData);
        delegate void RemoveItemEvent(RemoveItemOperation eventData);
        event AddItemEvent OnItemAdded;
        event RemoveItemEvent OnItemRemoved;

        ////// ACCESSORS //////

        public IItem this[int index] 
        {
            get
            {
                if (index < 0 || index >= Slots.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return Slots[index].Item;
            }
        }
    }

    [Obsolete]
    public struct ItemOperationEventData
    {
        public ItemOperationEventData(IItemSlot slot, IItem item, int amount)
        {
            Slot = slot;
            Item = item;
            Amount = amount;
        }

        public IItemSlot Slot { get; private set; }
        public IItem Item { get; private set; }
        public int Amount { get; private set; }
    }
    
    [Serializable]
    /// <summary>
    /// Defines a named array segmentation.
    /// Offset + Length should be < to array length to avoid out of range exceptions.
    /// </summary>
    public struct InventorySegmentation
    {
        /// <summary>
        /// Name of the segment
        /// </summary>
        public string Name;

        /// <summary>
        /// Index on which the segment starts in the array
        /// </summary>
        public int Index;

        /// <summary>
        /// Length of the segment.
        /// </summary>
        public int Length;
    }
}