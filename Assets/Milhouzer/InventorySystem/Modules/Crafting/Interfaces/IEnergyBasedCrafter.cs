
namespace Milhouzer.InventorySystem.CraftingSystem
{

    public interface IEnergyBasedCrafter : ICrafter
    {
        public IInventory InputFuel { get; }
        AddItemOperation ProvideInputFuel(IItemStack item);
    }
}