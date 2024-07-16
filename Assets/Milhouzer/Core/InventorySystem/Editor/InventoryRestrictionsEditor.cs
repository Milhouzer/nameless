using UnityEditor;
using UnityEngine;
using Milhouzer.Core.InventorySystem.Restrictions;

namespace Milhouzer.Core.InventorySystem
{
    [CustomEditor(typeof(InventoryRestrictions))]
    public class InventoryRestrictionsEditor : Editor
    {
        private SerializedProperty restrictionsProperty;
        private InventoryRestrictions InventoryRestrictions;
        private string[] restrictionTypeNames;
        private int selectedTypeIndex = -1;

        private void OnEnable()
        {
            restrictionsProperty = serializedObject.FindProperty("Restrictions");
            InventoryRestrictions = (InventoryRestrictions)target;

            // Récupère tous les types dérivés de AddItemRestriction
            var restrictionTypes = GetAllDerivedTypes(typeof(AddItemRestriction));
            restrictionTypeNames = new string[restrictionTypes.Count];
            for (int i = 0; i < restrictionTypes.Count; i++)
            {
                restrictionTypeNames[i] = restrictionTypes[i].Name;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Add Item Restrictions", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            // EditorGUILayout.PropertyField(restrictionsProperty);

            // Dropdown pour sélectionner le type de restriction à ajouter
            selectedTypeIndex = EditorGUILayout.Popup(selectedTypeIndex, restrictionTypeNames);

            if (GUILayout.Button("Add Restriction") && selectedTypeIndex >= 0)
            {
                AddSelectedRestriction();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
        }

        // Méthode pour ajouter la restriction sélectionnée à la liste des restrictions
        private void AddSelectedRestriction()
        {
            var restrictionType = GetAllDerivedTypes(typeof(AddItemRestriction))[selectedTypeIndex];

            if (restrictionType != null)
            {
                AddItemRestriction restriction = (AddItemRestriction)System.Activator.CreateInstance(restrictionType);
                InventoryRestrictions.Restrictions.Add(restriction);
                selectedTypeIndex = -1;
            }
            else
            {
                Debug.LogError("Failed to create restriction of type: " + restrictionType);
            }
        }

        // Méthode pour récupérer tous les types dérivés d'une classe donnée
        private static System.Collections.Generic.List<System.Type> GetAllDerivedTypes(System.Type baseType)
        {
            var types = new System.Collections.Generic.List<System.Type>();
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var assemblyTypes = assembly.GetTypes();
                foreach (var type in assemblyTypes)
                {
                    if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
            }
            return types;
        }
    }
}
