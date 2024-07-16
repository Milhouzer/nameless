namespace Milhouzer.Core.InventorySystem.CraftingSystem
{
    public interface ICrafter
    {
        public CraftingProcess Process { get; }
        public IInventory InputIngredients { get; }
        public IInventory Output { get; }
        AddItemOperation ProvideInputIngredient(IItemStack item);
        bool IsCrafting { get; }
        public void TryStartCraft();
        public void StopCraft();
    }

}