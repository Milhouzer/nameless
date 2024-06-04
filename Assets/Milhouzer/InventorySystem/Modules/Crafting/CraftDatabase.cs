using Milhouzer.InventorySystem.CraftingSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using System;
using System.Diagnostics;

namespace Milhouzer.InventorySystem
{
    [CreateAssetMenu(fileName = "CraftDatabase", menuName = "Milhouzer/Inventory/Crafting", order = 0)]
    public class CraftDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct Ingredient
        {
            public string name;
            public int amount;

            public static implicit operator Ingredient(string ingredientName)
            {
                return new Ingredient { name = ingredientName, amount = 1 };
            }

            public Ingredient(string _name, int _amount) 
            {
                name = _name;
                amount = _amount;
            }
        }

        [SerializeField]
        public SerializedDictionary<string, List<Ingredient>> CookRecipes = new();

        [SerializeField]
        public Dictionary<string, List<string>> ReversedCookRecipes = new();

        /// <summary>
        /// O(n*m) where n is length of <paramref name="db"/> and is the sum of the ingredients for recipe n.
        /// Returns a dictionary where the keys are items and the values are items they can craft.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private void GetReversedRecipes(SerializedDictionary<string, List<Ingredient>> db, ref Dictionary<string, List<string>> reversed)
        {

            UnityEngine.Debug.Log("[GET REVERSED RECIPES]");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach(KeyValuePair<string, List<Ingredient>> Recipes in db)
            {
                foreach(Ingredient ingredient in Recipes.Value)
                {
                    if (!reversed.ContainsKey(ingredient.name))
                    {
                        reversed.Add(ingredient.name, new List<string>() { Recipes.Key });
                    }
                    else
                    {
                        reversed[ingredient.name].Add(Recipes.Key);
                    }
                }
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            UnityEngine.Debug.Log($"[GET REVERSED RECIPES] Elapsed Time: {elapsedTime}");
        }

        /// <summary>
        /// Exposed method to other components. Finds a possible recipe based on the items in the inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        /// <TODO>
        /// Write other methods like this, for instance FindRecipe(List<IItemStack>), etc.
        /// </TODO>
        public List<string> FindRecipes(IInventory inventory, CraftingProcess process)
        {
            List<Ingredient> ingredients = new();
            foreach(IItemSlot slot in inventory.Slots)
            {
                ingredients.Add(new Ingredient(slot.Item.Data.ID, slot.Stack.Amount));
            }
            return FindRecipes(ingredients, process);
        }


        /// <summary>
        /// Internal method to find recipes based on a list of ingredients.
        /// </summary>
        /// <param name="ingredients"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        private List<string> FindRecipes(List<Ingredient> ingredients, CraftingProcess process)
        {
            return FindCookRecipes(ingredients);
        }
        
        /// <summary>
        /// Find a cook recipe based on given ingredients
        /// </summary>
        /// <param name="ingredients"></param>
        /// <returns></returns>
        /// <remarks>
        /// ingredients in list must be unique in order to find all recipes
        /// </remarks>
        private List<string> FindCookRecipes(List<Ingredient> ingredients)
        {
            UnityEngine.Debug.Log("[FIND RECIPE] " + string.Join(",", ingredients.Select(x => x.name)));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // ============================ //

            List<string> possibleRecipes = new();
            Dictionary<string, int> inProgressRecipes = new Dictionary<string, int>();

            foreach(Ingredient ingredient in ingredients)
            {
                // Get the output the that can be formed based on the ingredient.
                Ingredient existingIngredient = ReversedCookRecipes.Keys.FirstOrDefault();
                ReversedCookRecipes.TryGetValue(ingredient.name, out List<string> outputs);
                if(outputs == null)
                {
                    UnityEngine.Debug.Log("[FIND RECIPE] No recipes found for " + ingredient.amount + " " + ingredient.name);
                    UnityEngine.Debug.Log("[FIND RECIPE] DEBUG " + string.Join(",", CookRecipes["puree"].Select(x => x.name + x.amount)));
                    continue;
                }
                
                // For each output, check if the ingredient amount is sufficient.
                // if true : directly add the recipe in the possibleRecipes list if ingredients needed count is 1,
                // otherwise keep count of ingredients provided for this recipe. If the counter goes down to 0, all
                // ingredients are provided and the recipe is possible. Remove from dict and add to possibleRecipes list.
                // else : recipe can't be made.
                
                UnityEngine.Debug.Log("[FIND RECIPE] Recipes found for " + ingredient.name + " : " + string.Join(",", outputs));
                foreach(string output in outputs)
                {
                    List<Ingredient> outputIngredients = CookRecipes[output];
                    // Check if ingredient.amount <= needed.Count
                    Ingredient needed = outputIngredients.Find(x => x.name == ingredient.name);
                    if(needed.amount > ingredient.amount)
                    {
                        UnityEngine.Debug.Log(ingredient.name + " can't make " + output);
                        continue;
                    }

                    if(outputIngredients.Count == 1)
                    {
                        possibleRecipes.Add(output);                
                        UnityEngine.Debug.Log("found possible recipe : " + output);
                    }
                    else
                    {
                        if(!inProgressRecipes.ContainsKey(output))
                            inProgressRecipes.Add(output, outputIngredients.Count);

                        inProgressRecipes[output] -= 1;
                        if(inProgressRecipes[output] == 0)
                        {
                            possibleRecipes.Add(output);
                            inProgressRecipes.Remove(output);
                            UnityEngine.Debug.Log("found possible recipe : " + output);
                        }else
                        {
                            UnityEngine.Debug.Log("found partial recipe : " + output + " from " + ingredient.name);
                        }
                    }
                }
            }
            
            // ============================ //

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            UnityEngine.Debug.Log($"[FIND RECIPE] Elapsed Time: {elapsedTime}");

            return possibleRecipes;
        }

        public List<Ingredient> debugIngredients = new();
        private void PrintRecipesInfos()
        {
            foreach(KeyValuePair<string, List<string>> Recipes in ReversedCookRecipes)
            {
                UnityEngine.Debug.Log(Recipes.Key + " : " + string.Join(",", Recipes.Value));
            }
            List<string> recipes = FindCookRecipes(debugIngredients);
            UnityEngine.Debug.Log("Recipe found : " + string.Join(",", recipes));
        }
        
        public void GetReversedRecipes()
        {
            ReversedCookRecipes = new();
            GetReversedRecipes(CookRecipes, ref ReversedCookRecipes);
            PrintRecipesInfos();
        }
    }
}