using System;

namespace Milhouzer.Core.InventorySystem
{
    public enum ItemCategory
    {
        Food,
        Fuel,
        Material,
    }

    [Flags]
    public enum ItemFlags
    {
        Liquid = 0x1,
        Eatable = 0x2,
        Consumable = 0x4,
        Vegetable = 0x8,
        Tools = 0x11,
    }
}