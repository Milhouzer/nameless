using UnityEngine;
using Milhouzer.InventorySystem;

namespace Milhouzer.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class RemoveItem : TaskBase
    {
        [SerializeField]
        protected new RemoveItemTaskData _data;
        public new RemoveItemTaskData Data => _data;

        protected ItemStack _itemStack;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as RemoveItemTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(RemoveItem)}: Invalid data type. Expected {nameof(RemoveItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");

             _itemStack = new ItemStack(_data.Item.Data, _data.Item.Amount);
        
        }
        public override void Start()
        {
            if(_data.Inventory != null)
            {
                RemoveItemOperation operation = _data.Inventory.RemoveItem(_itemStack);
                
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
        private string _name = "RemoveItem Task Data";
        public new string Name => _name;
        
        [HideInInspector]
        public InventoryBase Inventory;

        public ItemStackDefinition Item;

        public override void GetComponentsReferences(GameObject target)
        {
            Inventory = target.GetComponent<InventoryBase>();
        }
    }
}
