using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.Restrictions;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class RemoveItem : TaskBase
    {
        [SerializeField]
        protected new RemoveItemTaskData _data;
        public new RemoveItemTaskData Data => _data;

        protected ItemStack _itemStack;
        protected RemoveItemOperation result;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as RemoveItemTaskData;
            if(_data == null)  throw new System.InvalidOperationException($"Initialization failed for {nameof(RemoveItem)}: Invalid data type. Expected {nameof(RemoveItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");        
        }

        public override void Start()
        {
            if(_data.Target != null)
            {
                int index = _data.Target.FindFirstItem(_data.Restrictions);
                result = _data.Target.RemoveItem(index);
                
                switch(result.Result)
                {
                    case RemoveItemOperationResult.RemovedAll:
                        taskRunState = TaskRunState.Finished;
                        break;
                    case RemoveItemOperationResult.PartiallyRemoved:
                        taskRunState = TaskRunState.Finished;
                        break;
                    case RemoveItemOperationResult.RemovedNone:
                        taskRunState = TaskRunState.Failed;
                        break;
                }

                WriteDataOnBlackboard("REMOVED_ITEM", result);
            }
            else
            {
                taskRunState = TaskRunState.Failed;
            }
        }

        public override TaskRunState Execute()
        {
            return taskRunState;
        }

        public override bool Complete()
        {
            return true;
        }

        public override void Stop()
        {

        }

        public override ITaskData GetData()
        {
            return _data;
        }
    }

    [System.Serializable]
    public class RemoveItemTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "Remove Item";
        public new string Name => _name;

        [SerializeField]
        private string _inventoryName = "";
        public string InventoryName => _inventoryName;

        [SerializeField]
        private InventoryRestrictions _restrictions;
        public InventoryRestrictions Restrictions;
        
        public IInventory Target { get; private set; }

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Target = target.GetComponent<IInventory>();
        }
    }
}
