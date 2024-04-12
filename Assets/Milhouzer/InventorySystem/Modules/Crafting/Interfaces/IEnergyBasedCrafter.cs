
namespace Milhouzer.InventorySystem.CraftingSystem
{

    public interface IEnergyBasedCrafter : ICrafter
    {
        public InventoryBase InputFuel { get; }
        public float RemainingPower { get; }
        AddItemOperation ProvideInputFuel(IItemStack item);
    }
}