using System;
using System.Collections.Generic;
using Milhouzer.Core.AI;
using UnityEditor;
using UnityEngine;

namespace Milhouzer.Game.AI.Editor
{
    [CustomEditor(typeof(InteractionSequence))]
    public class InteractionSequenceEditor : UnityEditor.Editor
    {
        private SerializedProperty _tasks;
        private InteractionSequence Sequence;
        private SerializedProperty selectedTypeIndexProperty;
        List<Type> taskTypes = new();

        private string[] tasksTypeNames;
        private int selectedTypeIndex = 0;

        private void OnEnable()
        {
            _tasks = serializedObject.FindProperty("_tasks");
            Sequence = (InteractionSequence)target;

            // Récupère tous les types dérivés de AddItemRestriction
            taskTypes = GetAllDerivedTypes(typeof(TaskBase));
            tasksTypeNames = new string[taskTypes.Count];
            for (int i = 0; i < taskTypes.Count; i++)
            {
                tasksTypeNames[i] = taskTypes[i].Name;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Tasks have two target types : runner and target.", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("A task should be coded so the target does the action corresponding to the name of a task.", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(" ie: ProvideFuel => The target provides fuel from its inventory.", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add Task", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            // Dropdown pour sélectionner le type de restriction à ajouter
            selectedTypeIndex = EditorGUILayout.Popup(selectedTypeIndex, tasksTypeNames);

            if (GUILayout.Button("Add Task") && selectedTypeIndex >= 0)
            {
                AddSelectedTask();
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        // Méthode pour ajouter la Task sélectionnée à la liste des Tasks
        private void AddSelectedTask()
        {
            var taskType = taskTypes[selectedTypeIndex];

            if (taskType != null)
            {
                var task = (TaskBase)Activator.CreateInstance(taskType);
                var Task = (TaskBase)Convert.ChangeType(task, taskType);

                Sequence.Tasks.Add(Task);
                selectedTypeIndex = -1;
            }
            else
            {
                Debug.LogError("Failed to create restriction of type: " + taskType);
            }
        }

        // Méthode pour récupérer tous les types dérivés d'une classe donnée
        private static List<Type> GetAllDerivedTypes(Type baseType)
        {
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
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
