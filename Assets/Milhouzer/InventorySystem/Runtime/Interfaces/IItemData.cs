using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IItemData
    {
        public string ID { get; }
        public string DisplayName { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        public int MaxStack { get; }
        public GameObject RenderModel { get; }
        public ItemCategory Category { get; }
        public ReadOnlyCollection<ItemProperty> Properties { get; }
        public ItemProperty GetProperty(string name);
    }
}
