
using System;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [Serializable]
    public class IntItemProperty : ItemProperty
    {

        [ItemPropertyValue(SupportedItemPropertyType.Int), SerializeField]
        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public IntItemProperty(string key, int value) : base(key)
        {
            _key = key;
            _value = value;
        }

        public new int GetValue()
        {
            return _value;
        }
    }

    [Serializable]
    public class FloatItemProperty : ItemProperty
    {

        [ItemPropertyValue(SupportedItemPropertyType.Float), SerializeField]
        private float _value;
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public FloatItemProperty(string key, float value) : base(key)
        {
            _key = key;
            _value = value;
        }
        
        public new float GetValue()
        {
            return _value;
        }
    }

    [Serializable]
    public class StringItemProperty : ItemProperty
    {
        [ItemPropertyValue(SupportedItemPropertyType.String), SerializeField]
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public StringItemProperty(string key, string value) : base(key)
        {
            _key = key;
            _value = value;
        }
        
        public new string GetValue()
        {
            return _value;
        }
    }

    public enum SupportedItemPropertyType
    {
        Int,
        Float,
        String,
    }
}
