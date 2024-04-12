
using UnityEditor;
using UnityEngine;
namespace Milhouzer.InventorySystem
{

    [CustomEditor(typeof(InventoryBase))]
    public class InventoryBaseEditor : Editor {
        
        InventoryBase inventory;
        
        private void OnEnable()
        {
            inventory = (InventoryBase)target;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.LabelField(inventory.ListItems(), EditorStyles.boldLabel);
        }
    }
}
