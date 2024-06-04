
namespace Milhouzer.InventorySystem.CraftingSystem
{

    public interface IEnergyBasedCrafter : ICrafter
    {
        public InventoryBase InputFuel { get; }
        AddItemOperation ProvideInputFuel(IItemStack item);
    }
}