using UnityEditor;
using UnityEngine;

namespace Milhouzer.InventorySystem
{    
    [CustomPropertyDrawer(typeof(ItemPropertyValueAttribute))]
    public class ItemPropertyValueDrawer: PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            ItemPropertyValueAttribute valueAttribute = (ItemPropertyValueAttribute)attribute;
            switch(valueAttribute.propertyType)
            {
                case SupportedItemPropertyType.Int:
                    EditorGUILayout.IntField(property.intValue);
                    break;
                case SupportedItemPropertyType.Float:
                    EditorGUILayout.FloatField(property.floatValue);
                    break;
                case SupportedItemPropertyType.String:
                    EditorGUILayout.TextField(property.stringValue);
                    break;
            }
        }
    }
}