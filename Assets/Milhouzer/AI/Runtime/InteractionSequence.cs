using System.Collections.Generic;
using UnityEngine;


namespace Milhouzer.AI
{
    /// <summary>
    /// Wrapper around list of tasks.
    /// Allows data control.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "InteractionSequence", menuName = "Milhouzer/AI/InteractionSequence", order = 0)]
    public class InteractionSequence : ScriptableObject 
    {
        /// <summary>
        /// Name of the sequence
        /// </summary>
        [SerializeField]
        string _name;
        public string Name => _name;

        /// <summary>
        /// Tasks of the sequence
        /// </summary>
        /// <returns></returns>
        [SerializeReference]
        private List<TaskBase> _tasks = new();
        public List<TaskBase> Tasks => _tasks;

        /// <summary>
        /// Blackboard used by the sequence to store data.
        /// This can be useful to share data between tasks.
        /// </summary>
        private Blackboard _blackboard;
        public Blackboard Blackboard => _blackboard;

        /// <summary>
        /// Initialize the sequence : 
        /// - Instantiate all tasks and initializing them.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        public void Initialize(ITaskRunner runner, GameObject target)
        {
            if(runner == null)
                return;

            for (int i = 0; i < _tasks.Count; i++)
            {
                var task = _tasks[i];
                var taskData = task.GetData();

                task.Initialize(runner, target, taskData);

                _tasks[i] = task;
            }
        }
    }
}