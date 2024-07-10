using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Milhouzer.InventorySystem.CraftingSystem
{
    /// <summary>
    /// Crafting recipe class
    /// </summary>
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Milhouzer/Inventory/Crafting/Recipe", order = 0)]
    public class CraftingRecipe : ScriptableObject {
        public CraftingProcess Process;
        public List<string> Ingredients = new();
        public List<string> Result = new(); 

        public bool MatchIngredients(List<string> names)
        {
            return Ingredients.Intersect(names)
                              .Any();
        }
    }

    public enum CraftingProcess
    {
        Basic,
        Cook,
    }
}
