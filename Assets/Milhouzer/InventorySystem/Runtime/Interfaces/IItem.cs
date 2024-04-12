using System;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IItem
    {
        public IItemData Data { get; }
    }

    [System.Serializable]
    public struct ItemProperty
    {
        public string Key;
        public float FloatValue;
        public int IntValue;
        public string StringValue;
        public UnityEngine.Object ObjectReferenceValue;

        /// <TODO>
        /// Replace for non generic method and use a type selector.
        /// </TODO>
        public object GetValue<T>()
        {
			Type mType = typeof(T);

            if (typeof(T) == typeof(float))
            {
                return (T)(object)FloatValue;
            }
            else if (typeof(T) == typeof(int))
            {
                return (T)(object)IntValue;
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)StringValue;
            }
            else if (typeof(UnityEngine.Object).IsAssignableFrom(mType))
            {
                return ObjectReferenceValue;
            }
            else
            {
                throw new InvalidOperationException("Unsupported type requested.");
            }
        }
    }    
}
