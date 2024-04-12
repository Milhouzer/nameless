using UnityEngine;
using Milhouzer.InventorySystem;

namespace Milhouzer.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class CanPickupItem : TaskBase
    {
        [SerializeField]
        protected new CanPickupItemData _data;
        public new CanPickupItemData Data => _data;

        protected ItemStack _itemStack;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as CanPickupItemData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(CanPickupItem)}: Invalid data type. Expected {nameof(CanPickupItemData)}, but received {data?.GetType().Name ?? "null"}.");

             _itemStack = new ItemStack(_data.Item.Data, _data.Item.Amount);
        }

        public override void Start()
        {
            if(_data.Inventory != null)
            {
                AddItemOperation operation = _data.Inventory.AddItem(_itemStack);
                
                switch(operation.Result)
                {
                    case AddItemOperationResult.AddedAll:
                        taskRunState = TaskRunState.Finished;
                        break;
                    case AddItemOperationResult.PartiallyAdded:
                        taskRunState = TaskRunState.Finished;
                        break;
                    case AddItemOperationResult.AddedNone:
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
    public class CanPickupItemData : BaseTaskData
    {

        [SerializeField]
        private string _name = "CanPickupItem Task Data";
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
