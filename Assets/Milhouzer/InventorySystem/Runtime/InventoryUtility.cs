namespace Milhouzer.InventorySystem
{
    public static class InventoryUtility
    {
        public static ItemSlot FindItemByCategory(InventoryBase inventory, ItemCategory category)
        {
            return inventory.FindItem(x => x.Data.Category == category);
        }
    }
}
