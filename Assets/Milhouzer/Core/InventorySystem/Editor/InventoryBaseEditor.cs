
using UnityEditor;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{

    [CustomEditor(typeof(InventoryBase))]
    public class InventoryBaseEditor : Editor {
        
        InventoryBase inventory;
        
        protected virtual void OnEnable()
        {
            inventory = (InventoryBase)target;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.LabelField(inventory.ListItems(), EditorStyles.boldLabel);
        }
    }
}
