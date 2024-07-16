using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Milhouzer.Core.InventorySystem.Restrictions;

namespace Milhouzer.Core.InventorySystem
{
    public static class InventoryUtility
    {
        /// <summary>
        /// Find item in inventory with predicate on item slot.
        /// </summary>
        /// <param name="inventory">Inventory to search in</param>
        /// <param name="predicate">Predicate use in the search query.</param>
        /// <returns></returns>
        public static IItem FindItem(this IInventory inventory, Predicate<IItemSlot> predicate)
        {
            return inventory.Slots.Find(predicate)?.Stack.Item;
        }

        /// <summary>
        /// Find first item respecting predicate in IItemSlots
        /// </summary>
        /// <param name="slots">The list of IItemSlot objects</param>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>The index of the first matching item, or -1 if no match is found</returns>
        public static int FindItem(this List<IItemSlot> slots, Predicate<IItemSlot> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < slots.Count; i++)
            {
                if (predicate(slots[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Find slot in inventory based on a predicate.
        /// </summary>
        /// <param name="inventory">Inventory to search in</param>
        /// <param name="predicate">Predicate used in the search query</param>
        /// <returns></returns>
        public static IItemSlot FindItemSlot(this IInventory inventory, Predicate<IItemSlot> predicate)
        {
            /// <TODO>
            /// Way too many nested properties, make intermediate interface IItemSlot : ISlot<IItem>
            /// </TODO>
            return inventory.Slots.Find(predicate);
        }

        /// <summary>
        /// Find slot in inventory based on a predicate.
        /// </summary>
        /// <param name="inventory">Inventory to search in</param>
        /// <param name="predicate">Predicate used in the search query</param>
        /// <returns></returns>
        public static IItemSlot FindItemSlot(this List<IItemSlot> slots, Predicate<IItemSlot> predicate)
        {            
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return slots.Find(predicate);

        }

        /// <summary>
        /// Find first item respecting restrictions in inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="restrictions"></param>
        /// <returns></returns>
        public static int FindFirstItem(this IInventory inventory, InventoryRestrictions restrictions)
        {
            if(restrictions == null)
                return 0;
                
            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                if(restrictions.SatisfyRestrictions(inventory[i].Data))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Find first item respecting restrictions in inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="restrictions"></param>
        /// <returns></returns>
        public static int FindFirstItem(this List<IItemSlot> slots, InventoryRestrictions restrictions)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if(restrictions.SatisfyRestrictions(slots[i].Item.Data))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Find first item respecting restrictions in inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="restrictions"></param>
        /// <returns></returns>
        public static IItemSlot FindItemSlot(this IInventory inventory, InventoryRestrictions restrictions)
        {
            IItemSlot firstEmpty = null;
            foreach (IItemSlot slot in inventory.Slots)
            {
                if(firstEmpty == null && slot.IsEmpty())
                {
                    firstEmpty = slot;
                }
                if(restrictions.SatisfyRestrictions(slot.Item.Data))
                    return slot;
            }

            return firstEmpty;
        }

        /// <summary>
        /// Find the first slot containing the input item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IItemSlot FindSlot(this IInventory inventory, IItem item)
        {
            if(item == null || item.Data == null)
                return null;

            return inventory.Slots.Find(x => x.Stack.Item.Data.ID == item.Data.ID);
        }


        /// <summary>
        /// List items in inventory
        /// </summary>
        /// <returns>List of items to the format : <index>: <id>, <amount></returns>
        public static string ListItems(this IInventory inventory)
        {
            StringBuilder builder = new StringBuilder();
            foreach(IItemSlot slot in inventory.Slots)
            {
                if(slot.IsEmpty())
                    continue;

                builder.AppendLine($"{slot.Index}: {slot.Item.Data.ID}, {slot.Stack.Amount}");
            }

            return builder.ToString();
        }

        public static string ListItems(this IInventory inventory, string inventoryName)
        {
            ReadOnlyCollection<IItemSlot> slots = inventory.GetInventory(inventoryName);
            StringBuilder builder = new StringBuilder();
            foreach(IItemSlot slot in slots)
            {
                if(slot.IsEmpty())
                    continue;

                builder.Append(slot.IsEmpty() ? ";" : $"{slot.Item.Data.ID}");
            }

            return builder.ToString();
        }
    }
}
