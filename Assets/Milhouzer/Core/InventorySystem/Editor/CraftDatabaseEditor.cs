using UnityEngine;
using UnityEditor;

namespace Milhouzer.Core.InventorySystem
{
    [CustomEditor(typeof(CraftDatabase))]
    public class CraftDatabaseEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            CraftDatabase db = (CraftDatabase)target;

            
            // Afficher un bouton pour appeler GetReversedRecipes
            if (GUILayout.Button("Get Reversed Recipes")) {
                db.GetReversedRecipes();
            }
        }
    }
}