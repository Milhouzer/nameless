using UnityEditor;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem
{
    public class InventorySystemEditor : EditorWindow
    {
        private ScriptableObject itemDatabase;
        private SerializedObject serializedItemDatabase;
        private int selectedIndex = -1;
        private int lastSelectedIndex = -1;

        private Vector2 scrollPosition;

        private string newItemName;

        [MenuItem("Milhouzer/ItemDBEditor")]
        private static void OpenWindow()
        {
            var window = GetWindow<InventorySystemEditor>(false, "Inventory System");
            window.minSize = new Vector2(600, 400);
            window.Show();
        }

        private void OnEnable()
        {
            LoadItemDatabase();
        }

        private void Update()
        {
            Repaint();
        }

        private void LoadItemDatabase()
        {
            ItemDatabase _itemDatabase = AssetDatabase.LoadAssetAtPath<ItemDatabase>("Assets/Milhouzer/Core/InventorySystem/Databases/ItemDatabase.asset");
            if (_itemDatabase != null)
            {
                itemDatabase = _itemDatabase;
                serializedItemDatabase = new SerializedObject(itemDatabase);
            }
        }

        Editor editor;
        SerializedProperty prop;

        private void OnGUI()
        {
            // Display the selector for ItemDatabase

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.ObjectField("Item Database", itemDatabase, typeof(ItemDatabase), false);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();


            // Left side: Scrollable list of items
            EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.3f));
            EditorGUILayout.LabelField("Item List", EditorStyles.boldLabel);

            // If an ItemDatabase is selected, show its properties for editing
            if (itemDatabase != null)
            {
                var entries = serializedItemDatabase.FindProperty("Entries");

                DrawItemList(entries);

                DrawItemCreationSection();

                serializedItemDatabase.Update();
                serializedItemDatabase.ApplyModifiedProperties();
                EditorGUILayout.EndVertical();
                

                // Right side: Selected item properties
                
                
                if(lastSelectedIndex != selectedIndex)
                {
                    prop = entries.GetArrayElementAtIndex(selectedIndex);
                    if(prop.objectReferenceValue != null)
                    {
                        if(editor != null)
                        {
                            DestroyImmediate(editor);
                        }

                        editor = Editor.CreateEditor(prop.objectReferenceValue);
                    }
                    lastSelectedIndex = selectedIndex;
                }

                if(editor != null)
                {
                    editor.OnInspectorGUI();
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawItemCreationSection()
        {
            newItemName = EditorGUILayout.TextField(newItemName);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create item"))
            {
                CreateItem();
            }
            if (GUILayout.Button("Add item"))
            {
                AddItem();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Remove item"))
            {
                RemoveItem();
            }

            if (GUILayout.Button("Delete item"))
            {
                DeleteItem();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawItemList(SerializedProperty entries)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < entries.arraySize; i++)
            {
                SerializedProperty prop = entries.GetArrayElementAtIndex(i);

                BaseItemData baseItemData = (BaseItemData)prop.objectReferenceValue;
                if (GUILayout.Button(baseItemData.DisplayName, selectedIndex == i ? "Button" : EditorStyles.miniButton))
                {
                    selectedIndex = i;
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawItemPropertiesSection(SerializedProperty entries)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.7f));
            EditorGUILayout.LabelField("Item Properties", EditorStyles.boldLabel);
            if (selectedIndex != -1 && selectedIndex < entries.arraySize)
            {
                EditorGUILayout.PropertyField(entries.GetArrayElementAtIndex(selectedIndex), true);
                // DrawItemProperties(entries.GetArrayElementAtIndex(selectedIndex));
            }
            else
            {
                EditorGUILayout.HelpBox("Select an item from the list to view its properties.", MessageType.Info);
            }
            EditorGUILayout.EndVertical();
        }

        private void CreateItem()
        {
            string path = "Assets/Milhouzer/InventorySystem/Items/";
            string name = newItemName;

            if(!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path + name)))
            {
                return;
            }

            SerializedProperty entries = serializedItemDatabase.FindProperty("Entries");
            int i = entries.arraySize;
            BaseItemData baseItemData = ScriptableObject.CreateInstance<BaseItemData>();
            baseItemData.ID = name.Trim().ToLower().Replace(" ", "_");
            baseItemData.DisplayName = name;

            AssetDatabase.CreateAsset(baseItemData, path + name + ".asset");

            entries.InsertArrayElementAtIndex(i);
            entries.GetArrayElementAtIndex(i).objectReferenceValue = baseItemData;
            
            serializedItemDatabase.ApplyModifiedProperties();
        }

        private void AddItem()
        {
            string path = "Assets/Milhouzer/InventorySystem/Items/";
            if(string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path + newItemName)))
            {
                BaseItemData baseItemData = AssetDatabase.LoadAssetAtPath<BaseItemData>(path + newItemName + ".asset");

                if(baseItemData == null)
                {
                    Debug.Log(path + newItemName + " does exists but scriptable object is null");
                    return;
                }
                Debug.Log(path + newItemName + " does exists " + baseItemData);
                SerializedProperty entries = serializedItemDatabase.FindProperty("Entries");
                int i = entries.arraySize;
                entries.InsertArrayElementAtIndex(i);
                entries.GetArrayElementAtIndex(i).objectReferenceValue = baseItemData;
                
                serializedItemDatabase.ApplyModifiedProperties();
            }
            else
            {
                Debug.Log(path + newItemName + " does not exists.");
            }

        }

        private void RemoveItem()
        {
            SerializedProperty entries = serializedItemDatabase.FindProperty("Entries");
            entries.DeleteArrayElementAtIndex(selectedIndex);

            serializedItemDatabase.ApplyModifiedProperties();
        }

        private void DeleteItem()
        {
            SerializedProperty entries = serializedItemDatabase.FindProperty("Entries");
            var e = entries.GetArrayElementAtIndex(selectedIndex);

            BaseItemData baseItemData = (BaseItemData)e.objectReferenceValue;
            
            // Supprimer l'asset du disque
            string assetPath = AssetDatabase.GetAssetPath(baseItemData);
            AssetDatabase.DeleteAsset(assetPath);

            // Supprimer la référence de la liste
            entries.DeleteArrayElementAtIndex(selectedIndex);

            serializedItemDatabase.ApplyModifiedProperties();
        }

    }
}
