using System;
using UnityEditor;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    [CustomEditor(typeof(BaseItemData))]
    public class BaseItemDataEditor : Editor
    {
        SerializedProperty ID;
        SerializedProperty DisplayName;
        SerializedProperty Category;
        SerializedProperty Flags;
        SerializedProperty IsStackable;
        SerializedProperty MaxStack;
        SerializedProperty Icon;
        SerializedProperty RenderModel;
        SerializedProperty SupportedProcesses;


        SupportedItemPropertyType propertyType;
        string propertyName = "";
        object objectItemPropertyValue;
        int intItemPropertyValue;
        string stringItemPropertyValue;
        float floatItemPropertyValue;

        
        private void OnEnable() 
        {
            ID = serializedObject.FindProperty("_id");
            DisplayName = serializedObject.FindProperty("_displayName");
            Category = serializedObject.FindProperty("_category");
            Flags = serializedObject.FindProperty("_flags");
            IsStackable = serializedObject.FindProperty("_isStackable");
            MaxStack = serializedObject.FindProperty("_maxStack");
            Icon = serializedObject.FindProperty("_icon");
            RenderModel = serializedObject.FindProperty("_renderModel");
            SupportedProcesses = serializedObject.FindProperty("_supportedProcesses");
        }

        private Vector2 scrollPos = Vector2.zero;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            {
                EditorGUILayout.BeginVertical();
                {
                    DrawBaseItemProperties();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Item processing", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(SupportedProcesses);
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("Custom properties", EditorStyles.boldLabel);
                    DrawItemPropertiesSection();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawBaseItemProperties()
        {
            EditorGUILayout.LabelField("Base item properties", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Item ID", EditorStyles.label);
                EditorGUILayout.PropertyField(ID);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Item Name", EditorStyles.label);
                EditorGUILayout.PropertyField(DisplayName);
            }
            EditorGUILayout.EndHorizontal();
            

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(Category);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(Flags);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            bool stackable = IsStackable.boolValue;
            EditorGUILayout.PropertyField(IsStackable);
            EditorGUILayout.EndHorizontal();

            if(stackable)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(MaxStack);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(Icon);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(RenderModel);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawItemPropertiesSection()
        {
            DrawPropertiesList();
            propertyType = (SupportedItemPropertyType)EditorGUILayout.EnumPopup(propertyType);
            propertyName = EditorGUILayout.TextField("Property Name", propertyName);
            DrawPropertyValueField();
            if (GUILayout.Button("Add property"))
            {
                AddProperty();
            }
            if (GUILayout.Button("Clear properties"))
            {
                ClearProperties();
            }
        }

        SerializedProperty propertiesList;
        private void DrawPropertiesList()
        {
            propertiesList = serializedObject.FindProperty("_properties");
            for (int i = 0; i < propertiesList.arraySize; i++)
            {
                SerializedProperty prop = propertiesList.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(prop, true);

            }
        }

        private void DrawPropertyValueField()
        {
            switch(propertyType)
            {
                case SupportedItemPropertyType.Float:
                    floatItemPropertyValue = EditorGUILayout.FloatField("Float item value", floatItemPropertyValue);
                    break;
                case SupportedItemPropertyType.Int:
                    intItemPropertyValue = EditorGUILayout.IntField("Int item value", intItemPropertyValue);
                    break;
                case SupportedItemPropertyType.String:
                    stringItemPropertyValue = EditorGUILayout.TextField("String item value", stringItemPropertyValue);
                    break;
            }
        }


        private void AddProperty()
        {
            BaseItemData data = (BaseItemData)target;
            switch(propertyType)
            {
                case SupportedItemPropertyType.Float:
                    data.SetProperty(propertyName, floatItemPropertyValue);
                    break;
                case SupportedItemPropertyType.Int:
                    data.SetProperty(propertyName, intItemPropertyValue);
                    break;
                case SupportedItemPropertyType.String:
                    data.SetProperty(propertyName, stringItemPropertyValue);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }


        private void ClearProperties()
        {
            BaseItemData data = (BaseItemData)target;
            data.ClearProperties();
        }
    }
}
