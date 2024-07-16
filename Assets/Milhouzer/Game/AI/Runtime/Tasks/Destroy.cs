using UnityEngine;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI
{
    /// <summary>
    /// Destroy task : destroy the target gameObject of the task
    /// </summary>
    [System.Serializable]
    public class Destroy : TaskBase
    {
        [SerializeField]
        protected new DestroyData _data;

        public override void Start()
        {
            GameObject.Destroy(Target, _data.Delay);
            taskRunState = TaskRunState.Finished;
        }

        public override TaskRunState Execute()
        {
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

    /// <summary>
    /// Destroy task data
    /// </summary>
    [System.Serializable]
    public class DestroyData : BaseTaskData
    {
        [SerializeField]
        private string _name = "Destroy Task Data";
        public new string Name => _name;

        /// <summary>
        /// Delay before the destroy
        /// </summary>
        public float Delay = 0f;
        
        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {

        }
    }
}
