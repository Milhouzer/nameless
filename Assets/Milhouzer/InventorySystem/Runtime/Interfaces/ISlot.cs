namespace Milhouzer.InventorySystem
{
    public interface IItemSlot : ISlot<IItemStack>
    {
        public IItem Item { get; }
    }
    
    public interface ISlot<T>
    {
        public int Index { get; }
        public T Stack { get; }
    }
}
