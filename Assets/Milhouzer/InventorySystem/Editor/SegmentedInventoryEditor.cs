using UnityEngine;
using UnityEditor;

namespace Milhouzer.InventorySystem
{
    /// <TODO>
    /// Create a way to check whether segmentation is coherent with max slots count.
    /// </TODO>
    [CustomEditor(typeof(SegmentedInventory))]
    public class SegmentedInventoryEditor : InventoryBaseEditor {

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();  
        }
    }
}