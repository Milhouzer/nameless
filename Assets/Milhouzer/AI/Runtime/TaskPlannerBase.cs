using System.Collections.Generic;
using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Base class for tasks planners.
    /// </summary>
    public class TaskPlannerBase : ITaskPlanner, IUIDataSerializer
    {
        /// <summary>
        /// ITaskRunner on which tasks are schedulded.
        /// </summary>
        protected ITaskRunner _taskRunner;
        public ITaskRunner TaskRunner => _taskRunner;

        /// <summary>
        /// Tasks to schedule.
        /// </summary>
        protected List<ITask> _tasks;
        public List<ITask> Tasks => _tasks;
        
        public event AddTask_EventHandler TaskAddEvent;

        public TaskPlannerBase(ITaskRunner runner)
        {
            _taskRunner = runner;
            _tasks = new();
            runner.TaskAdd += TaskRunner_AddTask;
        }

        private void TaskRunner_AddTask(ITask task, ref bool Cancel)
        {
            Cancel = true;
            AddTask(task);
        }

        public void AddTask(ITask task)
        {
            _tasks.Add(task);
            Debug.Log("Task planned " + task);
            bool Cancel = false;
            TaskAddEvent?.Invoke(task, ref Cancel);
        }

        public void Interrupt()
        {
            _taskRunner.TaskAdd -= TaskRunner_AddTask;
        }

        public void Apply()
        {
            _taskRunner.TaskAdd -= TaskRunner_AddTask;
            _taskRunner.ExecutionMode = TaskRunnerExecutionMode.AutoExecute;
            _taskRunner.AddTasks(Tasks);
        }

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Type","TaskPlannerBase"},
            };
        }
    }
}
