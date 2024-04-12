using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IItemStack
    {
        BaseItem Item { get; }
        int Amount { get; }
        int MaxAmount { get; }

        AddItemOperation Add(int amount);
        RemoveItemOperation Remove(int amount);
    }
}
