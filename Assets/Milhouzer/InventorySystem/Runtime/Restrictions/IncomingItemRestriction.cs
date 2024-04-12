using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem.Restrictions
{    
    [System.Serializable]
    public class IncomingInventoryRestriction : AddItemRestriction
    {
        [SerializeField]
        List<InventoryBase> AcceptedInventories = new();

        public override bool IsSatisfied(InventoryBase inventory, IItemData item)
        {
            return AcceptedInventories.Contains(inventory);
        }
    }
}