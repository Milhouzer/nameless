using System;
using System.Linq;

namespace Milhouzer.InventorySystem
{
    public static class InventoryUtility
    {
        public static ItemSlot FindItemByCategory(this InventoryBase inventory, ItemCategory category)
        {
            return inventory.Slots.First(x => x.Data.Category == category);
        }

        public static IItem FindItem(this InventoryBase inventory, Predicate<ItemSlot> predicate)
        {
            return inventory.Slots.Find(predicate)?.Item;
        }

        public static ItemSlot FindSlot(this InventoryBase inventory, IItemStack stack)
        {
            return inventory.FindSlot(stack?.Item);
        }

        /// <summary>
        /// Find the first slot containing the input item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemSlot FindSlot(this InventoryBase inventory, IItem item)
        {
            if(item == null || item.Data == null)
                return null;

            ItemSlot slot =inventory.Slots.Find(x => x.Data.ID == item.Data.ID);
            return slot;
            
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
            //         if(item.Data.ID == slot.Stack.Item.Data.ID)
            //             return slot;
            //     }
            // }

            // // Update item stack on slot.
            // inventory.Slots[index] = new ItemSlot(new ItemStack(item.Data, 0), index);
            
            // return inventory.Slots[index];
        }
    }
}
