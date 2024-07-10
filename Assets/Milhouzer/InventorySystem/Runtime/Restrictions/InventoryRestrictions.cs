using System;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem.Restrictions
{
    [CreateAssetMenu(fileName = "InventoryRestrictions", menuName = "InventoryRestrictions", order = 0)]
    public class InventoryRestrictions : ScriptableObject {
        
            [SerializeReference]
            public List<AddItemRestriction> Restrictions = new();

            public bool SatisfyRestrictions(IItemData data)
            {
                foreach(AddItemRestriction restriction in Restrictions)
                {
                    if(!restriction.IsSatisfied(data))
                        return false;
                }

                return true;
            }
    }
}