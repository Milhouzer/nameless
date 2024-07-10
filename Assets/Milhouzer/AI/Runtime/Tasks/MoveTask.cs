using UnityEngine;
using UnityEngine.AI;

namespace Milhouzer.AI
{
    /// <summary>
    /// Move task : moves the target to a specified position using the GenericMovement component.
    /// </summary>
    [System.Serializable]
    public class MoveTask : TaskBase
    {
        [SerializeField]
        protected new MoveTaskData _data;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as MoveTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(MoveTask)}: Invalid data type. Expected {nameof(MoveTaskData)}, but received {data?.GetType().Name ?? "null"}.");
        }

        public override void Start()
        {
            if(_data.Movement != null)
            {
                NavMeshHit hit;
                NavMesh.SamplePosition(_data.Destination, out hit, 1f, NavMesh.AllAreas);

                _data.Destination = hit.position;
                _data.Movement.Move(_data.Destination);
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
            return Vector3.Distance(_data.Movement.transform.position, _data.Destination) < 0.1f;
        }
        
        public override ITaskData GetData()
        {
            return _data;
        }
    }
    
    [System.Serializable]
    public class MoveTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "Move Task Data";
        public new string Name => _name;
        
        [HideInInspector]
        public GenericMovement Movement;
        public Vector3 Destination;


        public MoveTaskData(Vector3 destination, GenericMovement movement)
        {
            Destination = destination;
            Movement = movement;
        }

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Movement = target.GetComponent<GenericMovement>();
        }
    }
}
