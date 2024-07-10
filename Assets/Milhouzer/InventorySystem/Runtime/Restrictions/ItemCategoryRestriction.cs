using System.Collections.Generic;
using UnityEngine;
using Milhouzer.InventorySystem.Restrictions;
using System.Linq;

namespace Milhouzer.InventorySystem
{
    [System.Serializable]
    public class ItemCategoryRestriction : AddItemRestriction
    {
        [SerializeField]
        List<ItemCategory> AcceptedCategories = new();

        public override bool IsSatisfied(IItemData item)
        {
            return AcceptedCategories.Contains(item.Category);
        }
    }
}
