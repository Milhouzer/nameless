namespace Milhouzer.Core.InventorySystem.Restrictions
{
    [System.Serializable]
    public abstract class AddItemRestriction
    {
        public abstract bool IsSatisfied(IItemData item);
    }
}