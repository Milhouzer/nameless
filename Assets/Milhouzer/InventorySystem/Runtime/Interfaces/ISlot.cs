namespace Milhouzer.InventorySystem
{
    public interface IItemSlot : ISlot<IItemStack>
    {
        IItem Item { get; }
        bool CanAddItem(IItem item);
    }
    
    public interface ISlot<T>
    {
        int Index { get; }
        T Stack { get; }
        bool IsEmpty();
        void Empty();
    }
}
