using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDropTable", menuName = "Milhouzer/Inventory/Item/ItemDropTable", order = 0)]
    public class ItemDropTable : DropTable<BaseItemData>
    {

        [System.Serializable]
        public class WeightedItem
        {
            public BaseItemData item;
            [Range(0f, 1f)] public float probability;
        }
            
            

        [SerializeField]
        private WeightedItem[] weightedItems;

        public ItemStack GetRandomUnitaryStack()
        {
            BaseItemData data = GetRandomElement();
            return new ItemStack(data, 1);
        }

        
        public override BaseItemData GetRandomElement()
        {      
            if (weightedItems == null || weightedItems.Length == 0)
            {
                Debug.LogError("ItemDropTable has no items configured.");
                return null;
            }

            // Calculate the total probability
            float totalProbability = 0f;
            foreach (var weightedItem in weightedItems)
            {
                totalProbability += weightedItem.probability;
            }

            // Randomly select an item based on probabilities
            float randomValue = Random.Range(0f, totalProbability);
            float cumulativeProbability = 0f;

            foreach (var weightedItem in weightedItems)
            {
                cumulativeProbability += weightedItem.probability;

                if (randomValue <= cumulativeProbability)
                {
                    return weightedItem.item;
                }
            }

            // Fallback: in case of any issue, return the last item
            return weightedItems[weightedItems.Length - 1].item;
        }
    }

    public abstract class DropTable<T> : ScriptableObject
    {
        public abstract T GetRandomElement();
    }
}
