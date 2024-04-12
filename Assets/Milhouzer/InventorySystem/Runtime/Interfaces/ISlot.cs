namespace Milhouzer.InventorySystem
{
    public interface ISlot
    {
        public int Index { get; }
        public IItemStack Stack { get; }
    }
    
    public interface ISlot<T> where T : IItemStack
    {
        public int Index { get; }
        public T Stack { get; }
    }
}
