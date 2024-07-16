using System;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    [Serializable]
    public class ItemProperty
    {

        [SerializeField]
        protected string _key;
        public string Key => _key;

        public object GetValue() { return null; }

        public ItemProperty(string key)
        {
            _key = key;
        }
    }
}