using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Interact task : interact with a IInteractable target.
    /// Select a sequence to execute by passing an int parameter
    /// </summary>
    [System.Serializable]
    public class InteractTask : TaskBase
    {

        [SerializeField]
        protected new InteractTaskData _data;

        private InteractionSequence _sequence;
        private int _current;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as InteractTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(InteractTask)}: Invalid data type. Expected {nameof(InteractTaskData)}, but received {data?.GetType().Name ?? "null"}.");
        }

        public override void Start()
        {
            if(!ReferenceEquals(_data.Interactable, null))
            {
                Debug.Log("Interact task : " + _data + ", " + _data.Index + " / " + _data.Interactable.Options.Count);
                _current = 0;

                _sequence = ScriptableObject.Instantiate(_data.Interactable.Options[_data.Index]);
                _sequence.Initialize(_runner, _target);
                _sequence.Tasks[_current].Start();

                taskRunState = TaskRunState.Running;
            }else
            {
                Debug.Log("Interact task : interactable null");
                taskRunState = TaskRunState.Failed;
            }
        }

        public override TaskRunState Execute()
        {
            TaskBase task = _sequence.Tasks[_current];
            /// <TODO>
            /// It takes 1 more exectuion to execute the task because the task in interacttask is in waiting state 
            /// so 1 execution to start and 1 to execute then.
            /// </TODO>
            Debug.Log("Execute interact task : " + task + ", " + _sequence.Tasks.Count + task.State );

            switch(task.State)
            {
                case TaskRunState.Waiting:
                    task.Start();
                    taskRunState = TaskRunState.Running;
                    break;
                case TaskRunState.Running:
                    task.Execute();
                    break;
                case TaskRunState.Finished:
                    Complete();
                    break;
                case TaskRunState.Failed:
                    Complete();
                    break;
            }
            Debug.Log("New task state " + task.State );

            return taskRunState;
        }

        public override void Stop()
        {

        }

        public override bool Complete()
        {
            _current++;

            if(_current == _sequence.Tasks.Count)
            {
                taskRunState = TaskRunState.Finished;
                return true;
            }

            taskRunState = TaskRunState.Waiting;
            
            return false;
        }
    }

    public class InteractTaskData : BaseTaskData
    {
        [SerializeField]
        private string _name = "Interact Task Data";
        public new string Name => _name;
        
        [HideInInspector]
        public IInteractable Interactable;

        private int _index = 0;
        public int Index => _index;


        public InteractTaskData(IInteractable interactable)
            : this(interactable, 0) { }

        public InteractTaskData(IInteractable interactable, int index)
        {
            Interactable = interactable;
            _index = index;
        }

        public override void GetComponentsReferences(GameObject target)
        {
            target.GetComponent<IInteractable>();
        }
    }
}
