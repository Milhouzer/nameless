using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Event triggered when a task is added to a taskrunner.
    /// </summary>
    /// <param name="task">Task to be added.</param>
    /// <param name="Cancel">True if the add is canceled. False otherwise.</param>
    public delegate void AddTask_EventHandler(ITask task, ref bool Cancel);

    /// <summary>
    /// Task runner interface. A task runner can execute sequences of tasks with different execution modes. 
    /// <see>TaskRunnerExecutionMode</see>
    /// </summary>
    public interface ITaskRunner
    {
        /// <summary>
        /// Does the runner starts automatically when instanciated.
        /// </summary>
        /// <value></value>
        bool AutoStart { get; }

        /// <summary>
        /// True if the runner is running.
        /// </summary>
        /// <value></value>
        bool IsRunning { get; }

        /// <summary>
        /// Index of the current task that is being executed.
        /// </summary>
        /// <value></value>
        int Current { get; }

        /// <summary>
        /// Reference to the GameObject that holds the TaskRunner component.
        /// </summary>
        /// <value></value>
        public GameObject Owner { get; }

        /// <summary>
        /// Tasks set on the runner.
        /// </summary>
        /// <value></value>
        /// <TODO>
        /// Use a TaskSequence (currently InteractionSequence) instead of a list.
        /// This allows a better flow control with functions like Step (we could imagine a reverse mode but somme actions are irreversible due to data loss)
        /// Reversable actions: move, rotate (newtonian mechanics).
        /// Irreversible actions: add/remove items, etc.
        /// </TODO>
        public ReadOnlyCollection<ITask> Tasks { get; }

        /// <summary>
        /// Execution mode of the runner.
        /// </summary>
        /// <value></value>
        public TaskRunnerExecutionMode ExecutionMode { get; set; }

        /// <summary>
        /// TaskAdd event.
        /// </summary>
        public event AddTask_EventHandler TaskAdd;

        /// <summary>
        /// Adds a task to the tasks collection.
        /// </summary>
        /// <param name="task"></param>
        void AddTask(ITask task);

        /// <summary>
        /// Add range of tasks.
        /// </summary>
        /// <param name="tasks"></param>
        void AddTasks(List<ITask> tasks);

        /// <summary>
        /// Start the runner.
        /// </summary>
        /// <param name="reset"></param>
        void StartRunner(bool reset = false);

        /// <summary>
        /// Execute the tasks in the collection
        /// </summary>
        void Run();

        /// <summary>
        /// Stop the runner
        /// </summary>
        void Stop();

        /// <summary>
        /// End the current task.
        /// </summary>
        void EndTask();

        /// <summary>
        /// Gets the next current index according to the task execution mode.
        /// </summary>
        void Next();

        /// <summary>
        /// Reset the runner : empty task queue and set Current to 0;
        /// </summary>
        void Reset();
    }
}
