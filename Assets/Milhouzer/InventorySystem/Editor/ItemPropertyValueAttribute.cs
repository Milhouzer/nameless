using System;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemPropertyValueAttribute : PropertyAttribute
    {
        public SupportedItemPropertyType propertyType;

        public ItemPropertyValueAttribute(SupportedItemPropertyType type)
        {
            propertyType = type;
        }
    }
}
