using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.Core.AI
{
    /// <summary>
    /// Task planner interface. Tasks planners are used to plan a sequence of tasks to be executed later by a runner as a sequence.
    /// </summary>
    public interface ITaskPlanner
    {
        /// <summary>
        /// Planned tasks
        /// </summary>
        /// <TODO>
        /// Change for readonlycollection.
        /// </TODO>
        public List<ITask> Tasks { get; }

        /// <summary>
        /// Runner that is planning the tasks.
        /// </summary>
        /// <value></value>
        public ITaskRunner TaskRunner { get; }
        
        /// <summary>
        /// Adds a task to the tasks collection.
        /// </summary>
        /// <param name="task"></param>
        void AddTask(ITask task);

        /// <summary>
        /// Interupt the planification.
        /// </summary>
        void Interrupt();
        
        /// <summary>
        /// Apply the plannning to the TaskRunner
        /// </summary>
        void Apply();
        public event AddTask_EventHandler TaskAddEvent;
    }

    /// <summary>
    /// Interface implemented by components that can set task on a runner
    /// </summary>
    /// <TODO>
    /// Remove, components should not be able to set tasks on a runner
    /// </TODO>
    public interface ITaskSetter
    {
        ITaskRunner TaskRunner { get; }
        void SetTask(ITask task);
    }
}
