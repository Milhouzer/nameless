using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// SetAnimatorState task : Set animator state of a IAnimator
    /// </summary>
    [System.Serializable]
    public class SetAnimatorState : TaskBase
    {
        [SerializeField]
        protected new SetAnimatorStateData _data;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as SetAnimatorStateData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(SetAnimatorState)}: Invalid data type. Expected {nameof(SetAnimatorStateData)}, but received {data?.GetType().Name ?? "null"}.");
        }

        public override void Start()
        {
            if(_data.Animator != null)
            {
                _data.Animator.SetState(_data.State);
                taskRunState = TaskRunState.Running;
            }else
            {
                taskRunState = TaskRunState.Failed;
            }
        }

        public override TaskRunState Execute()
        {
            if(Complete())
                taskRunState = TaskRunState.Finished;

            return taskRunState;
        }

        public override void Stop()
        {

        }

        public override bool Complete()
        {
            return true;
        }
        
        public override ITaskData GetData()
        {
            return _data;
        }
    }
    
    [System.Serializable]
    public class SetAnimatorStateData : BaseTaskData
    {

        [SerializeField]
        private string _name = "SetAnimatorState Task Data";
        public new string Name => _name;

        public string State;
        public IAnimator Animator;

        public override void GetComponentsReferences(GameObject target)
        {
            Animator = target.GetComponent<IAnimator>();
        }
    }
}
