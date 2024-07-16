using System;
using Milhouzer.Common.Utility;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Milhouzer/Inventory/Item/ItemDatabase", order = 0)]
    
    public class ItemDatabase : Database<BaseItemData>
    {
        
    }
}
