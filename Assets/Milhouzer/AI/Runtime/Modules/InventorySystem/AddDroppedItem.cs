using UnityEngine;
using Milhouzer.InventorySystem;

namespace Milhouzer.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class AddDroppedItem : TaskBase
    {
        [SerializeField]
        protected new AddDroppedItemTaskData _data;
        public new AddDroppedItemTaskData Data => _data;

        IItemHolder itemHolder;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as AddDroppedItemTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(AddDroppedItem)}: Invalid data type. Expected {nameof(AddItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");

            itemHolder = _runner.Owner.GetComponent<IItemHolder>();
        }

        public override void Start()
        {
            if(_data.Inventory != null && itemHolder != null)
            {
                RemoveItemOperation operation = itemHolder.Pickup(_data.Inventory);

                switch(operation.Result)
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
            }
            else
            {
                Debug.Log("Failed : " + _data.Inventory + ", " + itemHolder);
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
    public class AddDroppedItemTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "AddDroppedItem Task Data";
        public new string Name => _name;
        
        [HideInInspector]
        public IInventory Inventory;

        public override void GetComponentsReferences(GameObject target)
        {
            Inventory = target.GetComponent<IInventory>();
        }
    }
}
