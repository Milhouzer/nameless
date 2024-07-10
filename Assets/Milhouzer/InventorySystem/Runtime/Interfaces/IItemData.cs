using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.InventorySystem.ItemProcessing;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IItemData : IProcessable
    {
        /// <summary>
        /// Id of the item - Technical name.
        /// The item id should allow lower case characters, numbers and _ as special character only. No spaces.
        /// </summary>
        /// <value></value>
        public string ID { get; }

        /// <summary>
        /// Display name of the item
        /// </summary>
        /// <value></value>
        public string DisplayName { get; }

        /// <summary>
        /// Icon to be displayed in UI
        /// </summary>
        /// <value></value>
        public Sprite Sprite { get; }

        /// <summary>
        /// True if the item is stackable in inventories.
        /// </summary>
        /// <value></value>
        public bool IsStackable { get; }

        /// <summary>
        /// Max stack amount - ignored if <see cref="IsStackable"/> is set to false
        /// </summary>
        /// <value></value>
        public int MaxStack { get; }

        /// <summary>
        /// 3D model of the item rendered when instanced in the world.
        /// </summary>
        /// <value></value>
        public GameObject RenderModel { get; }

        /// <summary>
        /// Category of the item
        /// </summary>
        /// <value></value>
        public ItemCategory Category { get; }

        /// <summary>
        /// Get an item property from the property dictionary.
        /// </summary>
        /// <param name="key">name of the property</param>
        /// <returns><see cref="ItemProperty"/> corresponding to the key</returns>
        public ItemProperty GetProperty(string key);
    }
}
