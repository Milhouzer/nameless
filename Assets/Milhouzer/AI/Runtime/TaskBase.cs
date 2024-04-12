using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Base abstract class for every task.
    /// </summary>
    [System.Serializable]
    public abstract class TaskBase : ITask
    {
        /// <summary>
        /// Reference to the <see cref="ITaskRunner"/> that executes the task.
        /// </summary>
        protected ITaskRunner _runner;
        public ITaskRunner Runner => _runner;
        
        /// <summary>
        /// Reference to the target of the task
        /// </summary>
        protected GameObject _target;
        public GameObject Target => _target;

        /// <summary>
        /// Data used by this task
        /// </summary>
        protected ITaskData _data;
        public ITaskData Data => _data;

        /// <summary>
        /// State of the task.
        /// </summary>
        protected TaskRunState taskRunState = TaskRunState.Waiting;
        public TaskRunState State => taskRunState;

        /// <summary>
        /// Initialize the task. If TaskTargetType is Runner, target will be set to runner, another target otherwise
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="target"></param>
        /// <param name="data"></param>
        public void Initialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _runner = runner;
            _target = target;

            OnInitialize(runner, target, data);
            
            data.GetComponentsReferences(data.TargetType == TaskTargetType.Runner ? runner.Owner : target);
        }

        /// <summary>
        /// Method to override for child classes to initialize their own data.
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="target"></param>
        /// <param name="data"></param>
        protected virtual void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data) { }
        
        /// <summary>
        /// Start the task.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Execute the task.
        /// </summary>
        /// <returns></returns>
        public abstract TaskRunState Execute();

        /// <summary>
        /// Stop the task.
        /// </summary>
        public abstract void Stop();
        
        /// <summary>
        /// Complete the task.
        /// </summary>
        /// <returns></returns>
        public abstract bool Complete();
        
        /// <summary>
        /// Get the task data
        /// </summary>
        /// <returns></returns>
        public virtual ITaskData GetData()
        {
            return _data;
        }
    }

    public abstract class BaseTaskData : ITaskData
    {
        public string Name => "BaseTask";
        [SerializeField]
        private TaskTargetType _targetType = TaskTargetType.Target;
        public TaskTargetType TargetType => _targetType;

        [SerializeField]
        TaskFailureHandle _failureHandle;
        public TaskFailureHandle FailureHandle => _failureHandle;
        public abstract void GetComponentsReferences(GameObject target);
    }

}