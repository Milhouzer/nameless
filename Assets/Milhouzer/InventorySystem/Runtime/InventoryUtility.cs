using System;
using System.Linq;

namespace Milhouzer.InventorySystem
{
    public static class InventoryUtility
    {
        public static IItemSlot FindItemByCategory(this IInventory inventory, ItemCategory category)
        {
            /// <TODO>
            /// Way too many nested properties, make intermediate interface IItemSlot : ISlot<IItem>
            /// </TODO>
            return inventory.Slots.First(x => x.Stack.Item.Data.Category == category);
        }

        public static IItem FindItem(this IInventory inventory, Predicate<IItemSlot> predicate)
        {
            return inventory.Slots.Find(predicate)?.Stack.Item;
        }

        public static IItemSlot FindSlot(this IInventory inventory, IItemStack stack)
        {
            return inventory.FindSlot(stack?.Item);
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
            // return slot;
            
            // ItemSlot firstEmpty = null;
            // int index = 0;

            // for(int i = 0; i < inventory.Slots.Count; i++)
            // {
            //     ItemSlot slot = inventory.Slots[i];
            //     if(slot.Data == null)
            //     {
            //         if(firstEmpty == null)
            //         {
            //             firstEmpty = slot;
            //             index = i;
            //         }
            //     }else
            //     {
            //         if(item.Data.ID == slot.Item.Data.ID)
            //             return slot;
            //     }
            // }

            // // Update item stack on slot.
            // inventory.Slots[index] = new ItemSlot(new ItemStack(item.Data, 0), index);
            
            // return inventory.Slots[index];
        }
    }
}
