using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "BaseItemData", menuName = "Milhouzer/Inventory/Item/BaseItemData", order = 0)]
    public class BaseItemData : ScriptableObject, IItemData
    {
        [SerializeField] 
        private string _id;
        public string ID 
        {
            get { return _id; }
            set { _id = value; }
        }

        [SerializeField]
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        [SerializeField]
        private Sprite _icon;
        public Sprite Sprite
        {
            get { return _icon; }
            set { _icon = value; }
        }

        [SerializeField]
        bool _isStackable;
        public bool IsStackable 
        {
            get { return _isStackable;}
            set { _isStackable = value;}
        }

        [SerializeField]
        int _maxStack;
        public int MaxStack 
        {
            get { return _maxStack;}
            set { _maxStack = value;}
        }

        [SerializeField]
        GameObject _renderModel;
        public GameObject RenderModel 
        {
            get { return _renderModel; }
            set { _renderModel = value; }
        }

        [SerializeField]
        ItemCategory _category = ItemCategory.Food;
        public ItemCategory Category
        {
            get { return _category; }
            set { _category = value; }
        }

        [SerializeField]
        ItemFlags _flags = ItemFlags.Liquid & ItemFlags.Eatable;
        public ItemFlags Flags
        {
            get { return _flags; }
            set { _flags = value; }
        }

        [SerializeReference]
        private List<ItemProperty> _properties;


        public void SetProperty(string key, object value)
        {
            if(value is int intValue)
            {
                _properties.Add(new IntItemProperty(key, intValue));
            }
            if(value is float floatValue)
            {
                _properties.Add(new FloatItemProperty(key, floatValue));
            }
            if(value is string stringValue)
            {
                _properties.Add(new StringItemProperty(key, stringValue));
            }
        }

        public ItemProperty GetProperty(string key)
        {
            return _properties.Find(x => x.Key == key);
        }

        public void DeleteProperty(string key)
        {
            int index = _properties.FindIndex(x => x.Key == key);
            if (index != -1)
            {
                _properties.RemoveAt(index);
            }        
    }

        public ReadOnlyCollection<ItemProperty> GetProperties()
        {
            return new ReadOnlyCollection<ItemProperty>(_properties);
        }

        internal void ClearProperties()
        {
            _properties = new();
        }
    }
}