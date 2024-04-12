namespace Milhouzer.InventorySystem.CraftingSystem
{
    public interface ICrafter
    {
        public CraftingProcess Process { get; }
        public InventoryBase InputIngredients { get; }
        public InventoryBase Output { get; }
        AddItemOperation ProvideInputIngredient(IItemStack item);
        bool IsCrafting { get; }
        public bool TryStartCraft();
        public void StopCraft();
    }

}