using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Task interface
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Reference to the task runner that will execute the task.
        /// </summary>
        /// <value></value>
        ITaskRunner Runner { get; }

        /// <summary>
        /// Reference to the target of the task. ie: for a move task the target is self but for a AddItem task, the target 
        /// can be any object holding an inventory.
        /// </summary>
        /// <value></value>
        /// <TODO>
        /// Handle multi targets.
        /// </TODO>
        GameObject Target { get; }

        /// <summary>
        /// Task data. Holds references used in the task execution.
        /// </summary>
        /// <value></value>
        ITaskData Data { get; }

        /// <summary>
        /// State of the task.
        /// </summary>
        /// <value></value>
        TaskRunState State { get; }

        /// <summary>
        /// Initialize the task. This method should be called before the execution.
        /// </summary>
        /// <param name="runner">Runner that runs the task.</param>
        /// <param name="target">Target of the task. <see>Target</see></param>
        /// <param name="data">Data of the task.</param>
        void Initialize(ITaskRunner runner, GameObject target, ITaskData data);

        /// <summary>
        /// Start the task.
        /// </summary>
        void Start();

        /// <summary>
        /// Execute the task.
        /// </summary>
        /// <returns></returns>
        TaskRunState Execute();

        /// <summary>
        /// Complete the task.
        /// </summary>
        /// <returns></returns>
        bool Complete();

        /// <summary>
        /// Stop the task.
        /// </summary>
        void Stop();

        /// <summary>
        /// Get the task data.
        /// </summary>
        /// <returns></returns>
        ITaskData GetData();
    }
}
