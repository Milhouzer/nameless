#nullable enable

namespace Milhouzer.Core.InventorySystem
{
    public interface IItemStack
    {
        IItem Item { get; }
        int Amount { get; }
        int MaxAmount { get; }

        AddItemOperation Add(int amount);
        RemoveItemOperation Remove(int amount);
    }
}
