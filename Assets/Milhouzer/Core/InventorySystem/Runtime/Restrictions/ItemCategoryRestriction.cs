using System.Collections.Generic;
using UnityEngine;
using Milhouzer.Core.InventorySystem.Restrictions;
using System.Linq;

namespace Milhouzer.Core.InventorySystem
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
