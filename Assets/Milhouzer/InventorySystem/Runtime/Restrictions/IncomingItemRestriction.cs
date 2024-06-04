using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem.Restrictions
{    
    [System.Serializable]
    public class IncomingInventoryRestriction : AddItemRestriction
    {
        [SerializeField]
        List<IInventory> AcceptedInventories = new();

        public override bool IsSatisfied(IInventory inventory, IItemData item)
        {
            return AcceptedInventories.Contains(inventory);
        }
    }
}