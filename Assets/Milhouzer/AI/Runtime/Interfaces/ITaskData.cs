using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Task data interface.
    /// </summary>
    public interface ITaskData
    {
        /// <summary>
        /// Name of the task.
        /// </summary>
        /// <value></value>
        string Name { get; }
        
        /// <summary>
        /// Type of the task target. <see>TaskTargetType</see>
        /// </summary>
        /// <value></value>
        public TaskTargetType TargetType { get; }

        /// <summary>
        /// Failure handle type. <see>TaskFailureHandle</see>
        /// </summary>
        /// <value></value>
        TaskFailureHandle FailureHandle { get; }

        /// <summary>
        /// Get useful components references for the task execution on the specified target.
        /// </summary>
        /// <param name="target">Target to get the references from.</param>
        abstract void GetComponentsReferences(GameObject target, GameObject instigator);
    }
}
