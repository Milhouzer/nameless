using System;
using System.Collections.Generic;
using Milhouzer.Core.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem {

    [CreateAssetMenu(fileName = "InventoryDataInjector", menuName = "InventoryDataInjector", order = 0)]
    public class InventoryDataInjector : ScriptableObject {
        [SerializeField]
        public List<SlotData> SlotsData = new();
    }

    [Serializable]
    public struct SlotData {
        public BaseItemData ItemData;
        [Range(1, 100)]
        public int Amount;
        public InventoryRestrictions Restrictions;
        
    }
}