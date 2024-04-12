using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem.Restrictions
{
    public class InventoryRestrictions : MonoBehaviour
    {
        [SerializeReference]
        public List<AddItemRestriction> Restrictions;

        public bool SatisfyRestrictions(InventoryBase inventoryBase, IItemData data)
        {
            foreach(AddItemRestriction restriction in Restrictions)
            {
                if(!restriction.IsSatisfied(inventoryBase, data))
                    return false;
            }

            return true;
        }
    }
}
