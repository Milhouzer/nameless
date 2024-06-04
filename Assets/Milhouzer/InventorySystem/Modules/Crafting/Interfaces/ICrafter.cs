namespace Milhouzer.InventorySystem.CraftingSystem
{
    public interface ICrafter
    {
        public CraftingProcess Process { get; }
        public InventoryBase InputIngredients { get; }
        public InventoryBase Output { get; }
        AddItemOperation ProvideInputIngredient(IItemStack item);
        bool IsCrafting { get; }
        public void TryStartCraft();
        public void StopCraft();
    }

}