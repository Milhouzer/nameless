namespace Milhouzer.InventorySystem.Restrictions
{
    [System.Serializable]
    public abstract class AddItemRestriction
    {
        public abstract bool IsSatisfied(InventoryBase inventory, IItemData item);
    }
}