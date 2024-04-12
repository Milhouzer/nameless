using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    
    [CreateAssetMenu(fileName = "BaseItemData", menuName = "Milhouzer/Inventory/Item/BaseItemData", order = 0)]
    public class BaseItemData : ScriptableObject, IItemData
    {
        [SerializeField] 
        private string id;
        public string ID => id; 
        [SerializeField]
        private string _displayName;
        public string DisplayName => _displayName;
        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField]
        bool _isStackable;
        public bool IsStackable => _isStackable;

        [SerializeField]
        int _maxStack;
        public int MaxStack => _maxStack;

        [SerializeField]
        GameObject _renderModel;
        public GameObject RenderModel => _renderModel;

        [SerializeField]
        ItemCategory _category = ItemCategory.Food;
        public ItemCategory Category => _category;

        
        [SerializeField]
        List<ItemProperty> _properties;
        public ReadOnlyCollection<ItemProperty> Properties => _properties.AsReadOnly();

        public ItemProperty GetProperty(string name)
        {
            return _properties.Find(x => x.Key == name);
        }

        
    }
}